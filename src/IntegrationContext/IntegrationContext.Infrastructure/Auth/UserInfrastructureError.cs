using Library.Models;

namespace IntegrationContext.Infrastructure.Auth;

public static class UserInfrastructureError
{
    public static Error UserNotFound => new(nameof(UserNotFound), "The user is not found");
    public static Error FailedToAuthenticateGitea => new(nameof(FailedToAuthenticateGitea), "Failed to authenticate to Gitea");
    public static Error RefreshTokenIsNull => new(nameof(RefreshTokenIsNull), "The refresh token for the user is not set");
}
