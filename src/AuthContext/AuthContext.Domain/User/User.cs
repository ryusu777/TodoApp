using AuthContext.Domain.User.Entities;
using AuthContext.Domain.User.Events;
using AuthContext.Domain.User.ValueObjects;
using Library.Models;

namespace AuthContext.Domain.User;

public class User : AggregateRoot<UserId>
{
#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    private List<UserRefreshToken> RefreshToken = new();

    public Email Email { get; private set; }

    private User(
        UserId username,
        Email email,
        List<UserRefreshToken>? refreshToken = null
    ) : base(username)
    {
        Email = email;
        if (refreshToken is not null)
            RefreshToken = refreshToken;
    }

    public static User Create(
        UserId username,
        Email email,
        List<UserRefreshToken>? refreshToken = null
    )
    {
        return new User(username, email, refreshToken);
    }

    public void DeleteExpiredRefreshTokens()
    {
        foreach (var token in RefreshToken) 
        {
            if (token.IsExpired()) 
            {
                RefreshToken.Remove(token);
                RaiseDomainEvent(new RefreshTokenDeleted(token));
            }
        }
    }

    public Result RevokeRefreshToken(Jti jti)
    {
        UserRefreshToken? foundToken = RefreshToken
            .FirstOrDefault(e => e.Id == jti);

        if (foundToken is null)
            return UserDomainError.RefreshTokenNotFound;

        RefreshToken.Remove(foundToken);

        RaiseDomainEvent(new RefreshTokenRevoked(foundToken));
        DeleteExpiredRefreshTokens();

        return Result.Success();
    }

    public Result AddRefreshToken(
        Jti jti, 
        RefreshToken refreshToken, 
        DateTime expiresAt)
    {
        if (RefreshToken.Any(e => e.Id == jti)) 
            return UserDomainError.RefreshTokenExists;

        UserRefreshToken token = UserRefreshToken
            .Create(jti, refreshToken, expiresAt);

        RefreshToken
            .Add(token);

        RaiseDomainEvent(new RefreshTokenCreated(token));
        DeleteExpiredRefreshTokens();

        return Result.Success();
    }
}
