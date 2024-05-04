using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;
using MediatR;

namespace IntegrationContext.Application.Events.Handlers;

public class AccessTokenGrantedHandler : INotificationHandler<AccessTokenGranted>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGiteaAuthenticationService _giteaAuthService;
    private readonly IUserRepository _userRepository;

    public AccessTokenGrantedHandler(IGiteaAuthenticationService giteaAuthService, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _giteaAuthService = giteaAuthService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task Handle(AccessTokenGranted notification, CancellationToken cancellationToken)
    {
        Result<GiteaAuthenticatedUser>? giteaAuthenticatedUser = await _giteaAuthService
            .GetAuthenticatedUser(
                notification.JwtToken, 
                cancellationToken);

        if (giteaAuthenticatedUser.IsFailure 
            || giteaAuthenticatedUser.Value is null)
            return;

        Result<GiteaUser> foundUser = await _userRepository
            .GetGiteaUserByGiteaUserId(
                GiteaUserId.Create(giteaAuthenticatedUser.Value.id), 
                cancellationToken);

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
            Result<DateTime> jwtExpiry = _giteaAuthService
                .GetExpiredDateTime(notification.JwtToken.Value);
            Result<DateTime> refreshTokenExpiry = _giteaAuthService
                .GetExpiredDateTime(notification.RefreshToken.Value);

            foundUser.Value.RefreshTokens(
                notification.JwtToken,
                notification.RefreshToken,
                jwtExpiry.Value,
                refreshTokenExpiry.Value
            );

            _unitOfWork.AddEventsQueue(foundUser.Value.DomainEvents);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

