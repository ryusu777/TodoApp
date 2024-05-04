using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.Auth;

public interface IGiteaAuthenticationService
{
    public Task<Result<GiteaAuthenticationResult>> RefreshTokenAsync(UserId username, CancellationToken ct);
    public Task<Result<GiteaAuthenticationResult>> GrantAccessTokenAsync(string verifyCode, CancellationToken ct);
    public Task<Result<GiteaClientCredentials>> GetClientCredentials(CancellationToken ct);
}
