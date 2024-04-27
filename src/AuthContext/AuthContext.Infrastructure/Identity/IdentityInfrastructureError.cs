using Library.Models;

namespace AuthContext.Infrastructure.Identity;

public static class IdentityInfrastructureError
{
    public static Error IncorrectUsernameOrPassword => new(nameof(IncorrectUsernameOrPassword), "Incorrect username or password");
    public static Error InvalidRefreshToken => new(nameof(InvalidRefreshToken), "Invalid Refresh Token");
    public static Error InvalidRefreshTokenOrJwtToken => new(nameof(InvalidRefreshTokenOrJwtToken), "Invalid JWT Token or Refresh Token");
    public static Error IssuedUserNotFound => new(nameof(IssuedUserNotFound), "The user issued in the token does not exists");
}
