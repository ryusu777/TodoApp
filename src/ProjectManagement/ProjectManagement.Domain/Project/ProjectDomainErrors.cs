using Library.Models;

namespace ProjectManagement.Domain.Project;

public static class ProjectDomainErrors
{
    public static Error PhaseAlreadyExists => new(nameof(PhaseAlreadyExists), "The phase already exists in the project");
    public static Error PhaseNotFound => new(nameof(PhaseNotFound), "The phase is not found");
    public static Error ProjectNotFound => new(nameof(ProjectNotFound), "The project is not found");
    public static Error FailedToSyncMembers(string message) => new(nameof(FailedToSyncMembers), "Failed to sync project members with Gitea repository: " + message);
}
