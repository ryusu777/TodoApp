using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.Auth;

public class GiteaUser : AggregateRoot<GiteaUserId>
{
#pragma warning disable CS8618
    private GiteaUser() { }
#pragma warning restore CS8618

    public UserId UserId { get; private set; }
    public JwtToken? JwtToken { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }
    public DateTime? JwtExpiresAt { get; private set; }
    public DateTime? RefreshTokenExpiresAt { get; private set; }

    public void RefreshTokens(
        JwtToken jwtToken, 
        RefreshToken refreshToken,
        DateTime jwtExpiresAt,
        DateTime refreshTokenExpiresAt)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
        JwtExpiresAt = jwtExpiresAt;
        RefreshTokenExpiresAt = refreshTokenExpiresAt;

        RaiseDomainEvent(new GiteaUserTokenRefreshed(this));
    }
}
