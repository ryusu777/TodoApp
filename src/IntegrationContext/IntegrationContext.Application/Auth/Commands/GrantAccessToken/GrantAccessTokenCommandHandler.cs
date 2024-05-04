using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth.Events;
using Library.Models;
using MediatR;

namespace IntegrationContext.Application.Auth.Commands.GrantAccessToken;

public class GrantAccessTokenCommandHandler : ICommandHandler<GrantAccessTokenCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGiteaAuthenticationService _giteaAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;

    public GrantAccessTokenCommandHandler(IGiteaAuthenticationService giteaAuthService, IUserRepository userRepository, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _giteaAuthService = giteaAuthService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(GrantAccessTokenCommand request, CancellationToken cancellationToken)
    {
        Result<GiteaAuthenticationResult> grantResult = await _giteaAuthService
            .GrantAccessTokenAsync(request.GrantCode, 
                cancellationToken);

        if (grantResult.IsFailure || grantResult.Value is null)
            return grantResult.Error;

        GiteaAuthenticationResult tokens = grantResult.Value;

        _unitOfWork
            .AddEventQueue(new AccessTokenGranted(
                tokens.JwtToken,
                tokens.RefreshToken
            ));

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
