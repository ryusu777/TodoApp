using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthContext.Application.Identity;
using AuthContext.Application.Identity.Model;
using AuthContext.Domain.User.ValueObjects;
using AuthContext.Infrastructure.Identity.Entities;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthContext.Infrastructure.Identity;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly JwtOptions _jwtOptions;

    public AuthenticationService(UserManager<AppIdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    private string GetJtiString(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claim = jwtToken.Claims
            .FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Jti);

        return claim?.Value ?? "";
    }

    private SigningCredentials GetSigningCredentials() 
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );

        return signingCredentials;
    }

    public Jti GetJti(JwtToken token)
    {
        return Jti.Create(GetJtiString(token.Value));
    }

    public Jti GetJti(RefreshToken token)
    {
        return Jti.Create(GetJtiString(token.Value));
    }

    public string GetUsername(JwtToken jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken.Value);
        var claim = token.Claims
            .FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Sub);

        return claim?.Value ?? "";
    }

    public async Task<Result<AuthenticationResult>> RefreshTokenAsync(JwtToken jwtToken, RefreshToken refreshToken, CancellationToken ct)
    {
        var handler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = handler.ValidateToken(refreshToken.Value, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = GetSigningCredentials().Key
        }, out SecurityToken readRefreshToken);

        Claim? jtiClaim = principal.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Jti);

        if (principal is null || jtiClaim is null)
            return Result.Failure<AuthenticationResult>(IdentityInfrastructureError.InvalidRefreshToken);

        Jti jwtJti = GetJti(jwtToken);
        Jti refreshTokenJti = GetJti(refreshToken);

        if (jwtJti != refreshToken)
            return Result.Failure<AuthenticationResult>(IdentityInfrastructureError.InvalidRefreshTokenOrJwtToken);

        string username = GetUsername(jwtToken);

        AppIdentityUser? user = await _userManager.Users.FirstOrDefaultAsync(e => e.UserName == username);

        if (user is null)
            return Result.Failure<AuthenticationResult>(IdentityInfrastructureError.IssuedUserNotFound);

        return Result.Success(GenerateAuthenticationResult(user));
    }

    public async Task<Result<AuthenticationResult>> SignInAsync(string username, string password, CancellationToken ct)
    {
        var user = await _userManager
            .Users
            .FirstOrDefaultAsync(e => e.UserName == username);

        if (user is null)
            return Result
                .Failure<AuthenticationResult>(IdentityInfrastructureError.IncorrectUsernameOrPassword);

        var isPasswordCorrect = await _userManager
            .CheckPasswordAsync(user, password);

        if (!isPasswordCorrect)
            return Result
                .Failure<AuthenticationResult>(IdentityInfrastructureError.IncorrectUsernameOrPassword);

        return Result.Success(GenerateAuthenticationResult(user));
    }

    private AuthenticationResult GenerateAuthenticationResult(
        AppIdentityUser user,
        params Claim[] additionalClaims
    )
    {
        Jti jti = Jti.CreateUnique();

        var claims = new Claim[] 
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, jti.Value),
        }.Concat(additionalClaims);
        
        var signingCredentials = GetSigningCredentials();

        var jwtExpiredDate = DateTime.UtcNow.AddMinutes(_jwtOptions.JwtExpiresInMinutes);
        
        var jwtToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            jwtExpiredDate,
            signingCredentials);

        var refreshToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiresInDays),
            signingCredentials); 

        var tokenHandler = new JwtSecurityTokenHandler();

        string jwtTokenString = tokenHandler
            .WriteToken(jwtToken);

        string refreshTokenString = tokenHandler
            .WriteToken(refreshToken);

        return new AuthenticationResult(
                jwtTokenString, 
                _jwtOptions.JwtExpiresInMinutes * 60 * 1000,
                "bearer",
                refreshTokenString);
    }

    public async Task<Result<string>> RequestChangePasswordAsync(string username, CancellationToken ct)
    {
        AppIdentityUser user = await _userManager.Users.FirstAsync(e => e.UserName == username, ct);

        return Result.Success(await _userManager.GeneratePasswordResetTokenAsync(user));
    }
}
