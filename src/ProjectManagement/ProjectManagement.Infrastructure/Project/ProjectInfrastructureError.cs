using Library.Models;

namespace ProjectManagement.Infrastructure.Project;

public static class ProjectInfrastructureError
{
	public static Error ProjectNotFound => new Error(nameof(ProjectNotFound), "The project is not found");
}
