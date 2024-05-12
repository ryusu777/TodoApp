using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;
using MassTransit;
using MassTransitContracts.GrantAccessToken;
using MediatR;

namespace IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;

public class GrantAccessTokenConsumer : IConsumer<GrantAccessTokenRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGiteaAuthenticationService _giteaAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;
    public GrantAccessTokenConsumer(IGiteaAuthenticationService giteaAuthService, IUserRepository userRepository, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _giteaAuthService = giteaAuthService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<GrantAccessTokenRequest> context)
    {
        Result<GiteaAuthenticationResult> grantResult = await _giteaAuthService
            .GrantAccessTokenAsync(context.Message.Code, context.CancellationToken);

        if (grantResult.IsFailure || grantResult.Value is null)
        {
            await context.RespondAsync(new GrantAccessTokenResponse(false, "", "", grantResult.Error.Code, grantResult.Error.Description));
            return;
        }

        Result<GiteaAuthenticatedUser>? giteaAuthenticatedUser = await _giteaAuthService
            .GetAuthenticatedUser(
                grantResult.Value.JwtToken, 
                context.CancellationToken);

        if (giteaAuthenticatedUser.IsFailure 
            || giteaAuthenticatedUser.Value is null)
        {
            await context.RespondAsync(new GrantAccessTokenResponse(false, "", "",
                giteaAuthenticatedUser.Error.Code, 
                giteaAuthenticatedUser.Error.Description));
            return;
        }

        Result<GiteaUser> foundUser = await _userRepository
            .GetGiteaUserByGiteaUserId(
                GiteaUserId.Create(giteaAuthenticatedUser.Value.id), 
                context.CancellationToken);

        if (foundUser.Error == AuthDomainError.UserNotFound)
        {
            var newUser = GiteaUser
                .Create(
                    GiteaUserId.Create(giteaAuthenticatedUser.Value.id), 
                    UserId.Create(giteaAuthenticatedUser.Value.login));

            _unitOfWork.AddEventQueue(new GiteaUserCreated(newUser));
        }
        else if (foundUser.Value is not null)
        {
            foundUser.Value.RefreshTokens(
                grantResult.Value.JwtToken,
                grantResult.Value.RefreshToken
            );

            _unitOfWork.AddEventsQueue(foundUser.Value.DomainEvents);
        }
        else 
        {
            await context.RespondAsync(new GrantAccessTokenResponse(false, "", "",
                foundUser.Error.Code, 
                foundUser.Error.Description));
            return;
        }

        Result persistResult = await _unitOfWork.SaveChangesAsync(context.CancellationToken);

        if (persistResult.IsFailure)
        {
            await context.RespondAsync(new GrantAccessTokenResponse(false, "", "",
                persistResult.Error.Code, 
                persistResult.Error.Description));
            return;
        }

        await context.RespondAsync(new GrantAccessTokenResponse(
            true, 
            giteaAuthenticatedUser.Value.login, 
            giteaAuthenticatedUser.Value.email,
            null, 
            null));
    }
}

