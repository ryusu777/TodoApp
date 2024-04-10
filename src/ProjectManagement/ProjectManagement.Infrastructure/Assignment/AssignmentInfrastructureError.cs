using Library.Models;

namespace ProjectManagement.Infrastructure.Assignment;

public static class AssignmentInfrastructureError
{
	public static Error AssignmentNotFound => new Error(nameof(AssignmentNotFound), "The assignment is not found");
}
