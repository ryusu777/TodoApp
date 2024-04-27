using AuthContext.Application.Identity.Model;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Application.Identity;

public interface IAuthenticationService
{
    public Task<Result<AuthenticationResult>> SignInAsync(
        string username, 
        string password, 
        CancellationToken ct);

    public Task<Result<AuthenticationResult>> RefreshTokenAsync(
        JwtToken jwtToken, 
        RefreshToken refreshToken, 
        CancellationToken ct);

    public Task<Result<string>> RequestChangePasswordAsync(
        string username, 
        CancellationToken ct);

    public string GetUsername(JwtToken jwtToken);

    public Jti GetJti(JwtToken token);
    public Jti GetJti(RefreshToken token);
}
