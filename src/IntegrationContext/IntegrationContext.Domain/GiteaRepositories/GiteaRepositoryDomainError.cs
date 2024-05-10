using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories;

public static class GiteaRepositoryDomainError
{
    public static Error FailedToCreateGiteaRepositoryHook 
        => new(nameof(FailedToCreateGiteaRepositoryHook), "Failed to create Gitea repository web hook");

    public static Error FailedToGetGiteaRepositories 
        => new(nameof(FailedToGetGiteaRepositories), "Failed to get Gitea repositories");
}
