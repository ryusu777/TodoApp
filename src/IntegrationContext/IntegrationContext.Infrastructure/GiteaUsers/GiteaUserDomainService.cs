using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Infrastructure.GiteaUsers;

public class GiteaUserDomainService : IGiteaUserDomainService
{
    private readonly IUserRepository _userRepository;
    private readonly IGiteaAuthenticationService _authenticationService;

	public GiteaUserDomainService(IGiteaAuthenticationService authenticationService, IUserRepository userRepository, IUnitOfWork unitOfWork)
	{
		_authenticationService = authenticationService;
		_userRepository = userRepository;
	}

	public async Task<Result<GiteaUser>> GetOrRefreshJwt(UserId userId, CancellationToken ct)
	{
        var user = await _userRepository.GetGiteaUserByUsername(userId, ct);

        if (user.IsFailure || user.Value is null)
            return Result.Failure<GiteaUser>(user.Error);

        if (user.Value.JwtToken is null || user.Value.RefreshToken is null)
            return Result.Failure<GiteaUser>(AuthDomainError.GiteaUserNotAuthenticated);

        if (!user.Value.JwtToken.IsExpired())
            return Result.Success(user.Value);

        if (user.Value.JwtToken.IsExpired() && !user.Value.RefreshToken.IsExpired())
        {
            var refreshTokenResult = await _authenticationService.RefreshTokenAsync(userId, ct);

            if (refreshTokenResult.IsFailure || refreshTokenResult.Value is null)
                return Result.Failure<GiteaUser>(refreshTokenResult.Error);

            user.Value.RefreshTokens(
                refreshTokenResult.Value.JwtToken, refreshTokenResult.Value.RefreshToken);

            return Result.Success(user.Value);
        }
        
        return Result.Failure<GiteaUser>(AuthDomainError.GiteaUserNotAuthenticated);
	}
}
