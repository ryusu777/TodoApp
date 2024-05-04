using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth;

public interface IGiteaAuthenticationService
{
    public Task<Result<GiteaAuthenticationResult>> RefreshTokenAsync(UserId username, CancellationToken ct);
    public Task<Result<GiteaAuthenticationResult>> GrantAccessTokenAsync(string verifyCode, CancellationToken ct);
    public Task<Result<GiteaClientCredentials>> GetClientCredentials(CancellationToken ct);
    public Task<Result<GiteaAuthenticatedUser>> GetAuthenticatedUser(JwtToken token, CancellationToken ct);
    public Result<DateTime> GetExpiredDateTime(string token);
}
