using Library.Models;

namespace IntegrationContext.Infrastructure.Gitea;

public static class GiteaUserInfrastructureError
{
    public static Error UserNotFound => new(nameof(UserNotFound), "The user is not found");
}
