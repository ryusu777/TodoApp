using Library.Models;

namespace AuthContext.Domain.User;

public static class UserDomainError
{
    public static Error RefreshTokenNotFound => new(nameof(RefreshTokenNotFound), "The refresh token is not found");
    public static Error RefreshTokenExists => new(nameof(RefreshTokenExists), "The refresh token is already exists");
}
