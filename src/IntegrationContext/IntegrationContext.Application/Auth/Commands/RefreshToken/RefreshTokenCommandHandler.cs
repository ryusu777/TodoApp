using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Abstractions.Messaging;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand>
{
    private readonly IGiteaAuthenticationService _giteaAuthService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IGiteaAuthenticationService giteaAuthService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _giteaAuthService = giteaAuthService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository
            .GetGiteaUserByUsername(UserId.Create(request.Username), cancellationToken);

        if (userResult.IsFailure || userResult.Value is null)
            return userResult.Error;

        var result = await _giteaAuthService
            .RefreshTokenAsync(UserId.Create(request.Username), cancellationToken);

        if (result.IsFailure || result.Value is null)
            return result.Error;

        var newTokens = result.Value;

        userResult.Value
            .RefreshTokens(
                newTokens.JwtToken,
                newTokens.RefreshToken,
                newTokens.JwtTokenExpiresAt,
                newTokens.RefreshTokenExpiresAt);

        return await _unitOfWork.SaveChangesAsync(userResult.Value.DomainEvents, cancellationToken);
    }
}

