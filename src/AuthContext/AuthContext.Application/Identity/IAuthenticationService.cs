using AuthContext.Application.Identity.Model;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;
using MassTransitContracts.GrantAccessToken;

namespace AuthContext.Application.Identity;

public interface IAuthenticationService
{
    public Task<Result> RegisterUserAsync(
        UserId username,
        Domain.User.ValueObjects.Email email,
        CancellationToken ct);

    public Task<Result<AuthenticationResult>> SignInAsync(
        string username, 
        string password, 
        CancellationToken ct);

    public Task<Result<AuthenticationResult>> SignInAsync(
        string username, 
        CancellationToken ct);

    public Task<bool> IsUserOnboarded(
        string username, 
        CancellationToken ct);

    public Task<Result<AuthenticationResult>> RefreshTokenAsync(
        JwtToken jwtToken, 
        RefreshToken refreshToken, 
        CancellationToken ct);

    public Task<Result<string>> RequestChangePasswordAsync(
        string username, 
        CancellationToken ct);

    public Task<Result> ChangePasswordAsync(
        string username, 
        string newPassword,
        string passwordResetToken,
        CancellationToken ct);

    public Task<Result<Uri>> GetGiteaAuthProviderUrl(Guid state, CancellationToken ct);

    public Task<Result<GrantAccessTokenResponse>> GrantGiteaAccessToken(string code, CancellationToken ct);

    public string GetUsername(JwtToken jwtToken);

    public Jti GetJti(JwtToken token);
    public Jti GetJti(RefreshToken token);
}
