using IntegrationContext.Domain.Auth.ValueObjects;

namespace IntegrationContext.Application.Auth.Models;

public record GiteaAuthenticationResult(
    JwtToken JwtToken,
    RefreshToken RefreshToken,
    DateTime JwtTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);
