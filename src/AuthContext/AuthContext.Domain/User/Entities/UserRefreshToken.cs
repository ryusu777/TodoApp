using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Domain.User.Entities;

public class UserRefreshToken : Entity<Jti>
{
#pragma warning disable CS8618
    private UserRefreshToken() { }
#pragma warning restore CS8618

    public RefreshToken RefreshToken { get; private set; }
    public DateTime ExpiresAt { get; private set; }

    private UserRefreshToken(
        Jti id,
        RefreshToken refreshToken,
        DateTime expiresAt) : base(id)
    {
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
    }

    public static UserRefreshToken Create(
        Jti id,
        RefreshToken refreshToken,
        DateTime expiresAt)
    {
        return new(id, refreshToken, expiresAt);
    }

    public bool IsExpired()
    {
        return DateTime.Now >= ExpiresAt;
    }
}
