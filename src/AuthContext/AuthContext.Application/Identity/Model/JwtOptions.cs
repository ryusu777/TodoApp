namespace AuthContext.Application.Identity.Model;

public record JwtOptions(
    string Issuer,
    string Audience,
    string SecretKey,
    int JwtExpiresInMinutes,
    int RefreshTokenExpiresInDays
);
