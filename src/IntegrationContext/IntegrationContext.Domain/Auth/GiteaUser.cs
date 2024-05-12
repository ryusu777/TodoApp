using IntegrationContext.Domain.Auth.Events;
using IntegrationContext.Domain.Auth.ValueObjects;
using Library.Models;

namespace IntegrationContext.Domain.Auth;

public class GiteaUser : AggregateRoot<GiteaUserId>
{
#pragma warning disable CS8618
    private GiteaUser() { }
#pragma warning restore CS8618

    private GiteaUser(
        GiteaUserId id,
        UserId username
    ): base(id) {
        UserId = username;
    }

    public UserId UserId { get; private set; }
    public JwtToken? JwtToken { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }

    public static GiteaUser Create(
        GiteaUserId id,
        UserId username
    ) {
        return new(id, username);
    }

    public void RefreshTokens(
        JwtToken jwtToken, 
        RefreshToken refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;

        RaiseDomainEvent(new GiteaUserTokenRefreshed(this));
    }
}
