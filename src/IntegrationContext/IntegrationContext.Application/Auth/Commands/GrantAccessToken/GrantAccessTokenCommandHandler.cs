using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth.Commands.GrantAccessToken;

public class GrantAccessTokenCommandHandler : ICommandHandler<GrantAccessTokenCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGiteaAuthenticationService _giteaAuthService;
    private readonly IUserRepository _userRepository;

    public GrantAccessTokenCommandHandler(IGiteaAuthenticationService giteaAuthService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _giteaAuthService = giteaAuthService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(GrantAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetGiteaUserByUsername(UserId.Create(request.Username), cancellationToken);

        if (userResult.IsFailure || userResult.Value is null)
            return userResult.Error;

        var grantResult = await _giteaAuthService
            .GrantAccessTokenAsync(UserId.Create(request.Username), 
                request.GrantCode, 
                cancellationToken);

        if (grantResult.IsFailure || grantResult.Value is null)
            return grantResult.Error;

        var tokens = grantResult.Value;

        userResult.Value
            .RefreshTokens(
                tokens.JwtToken, 
                tokens.RefreshToken, 
                tokens.JwtTokenExpiresAt, 
                tokens.RefreshTokenExpiresAt);

        return await _unitOfWork.SaveChangesAsync(userResult.Value.DomainEvents, cancellationToken);
    }
}
