using Library.Models;

namespace ProjectManagement.Domain.Project;

public static class ProjectDomainErrors
{
    public static Error PhaseAlreadyExists => new(nameof(PhaseAlreadyExists), "The phase already exists in the project");
    public static Error PhaseNotFound => new(nameof(PhaseNotFound), "The phase is not found");
}