using Library.Models;

namespace AuthContext.Domain.User;

public static class UserDomainError
{
    public static Error RefreshTokenNotFound => new(nameof(RefreshTokenNotFound), "The refresh token is not found");
    public static Error RefreshTokenExists => new(nameof(RefreshTokenExists), "The refresh token is already exists");
    public static Error IncorrectUsernameOrPassword => new(nameof(IncorrectUsernameOrPassword), "Incorrect username or password");
    public static Error InvalidRefreshToken => new(nameof(InvalidRefreshToken), "Invalid Refresh Token");
    public static Error InvalidRefreshTokenOrJwtToken => new(nameof(InvalidRefreshTokenOrJwtToken), "Invalid JWT Token or Refresh Token");
    public static Error IssuedUserNotFound => new(nameof(IssuedUserNotFound), "The user issued in the token does not exists");
    public static Error FailedToGetCredential => new(nameof(FailedToGetCredential), "Failed to get Gitea client credential");
    public static Error FailedToGrantAccessToken => new(nameof(FailedToGrantAccessToken), "Failed to grant Gitea access token");
    public static Error UserAlreadyOnboarded => new(nameof(UserAlreadyOnboarded), "The user is already registered in the system");
    public static Error UserNotFound = new (nameof(UserNotFound), "The user is not found");
}
