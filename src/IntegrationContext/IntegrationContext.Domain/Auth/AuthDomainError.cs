using Library.Models;

namespace IntegrationContext.Domain.Auth;

public static class AuthDomainError
{
    public static Error UserNotFound => new(nameof(UserNotFound), "The user is not found");
    public static Error FailedToFetchGiteaUser => new(nameof(FailedToFetchGiteaUser), "Failed to fetch the authenticated user to Gitea");
    public static Error InvalidToken => new(nameof(InvalidToken), "Invalid token");
}
