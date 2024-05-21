using Library.Models;

namespace IntegrationContext.Domain.GiteaRepositories;

public static class GiteaRepositoryDomainError
{
    public static Error FailedToCreateGiteaRepositoryHook 
        => new(nameof(FailedToCreateGiteaRepositoryHook), "Failed to create Gitea repository web hook");

    public static Error FailedToGetGiteaRepositories 
        => new(nameof(FailedToGetGiteaRepositories), "Failed to get Gitea repositories");

    public static Error FailedToGetGiteaAssignees(string message)
        => new(nameof(FailedToGetGiteaRepositories), "Failed to get Gitea repositories: " + message);

    public static Error GiteaRepositoryAlreadyAttached
        => new(nameof(GiteaRepositoryAlreadyAttached), "The gitea repository is already attached to the project");

    public static Error GiteaRepositoryNotFoundInTheProject
        => new(nameof(GiteaRepositoryNotFoundInTheProject), "The gitea repository is not found in the project");
}
