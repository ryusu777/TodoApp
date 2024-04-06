using Library.Models;

namespace ProjectManagement.Domain.Assignment;

public static class AssignmentDomainErrors
{
	public static Error AssigneeAlreadyExists => new(nameof(AssigneeAlreadyExists), "The assignee is already assigned to the task");
	public static Error AssigneeNotFound => new(nameof(AssigneeNotFound), "The assignee is not found in the current assignment");
	public static Error AssignmentIsNotAvailableToWorkOn => new(nameof(AssignmentIsNotAvailableToWorkOn), "The assignment is not available for work");
	public static Error CannotCancelWorkOnAssignment => new(nameof(CannotCancelWorkOnAssignment), "Cannot cancel working on the assignment");
	public static Error CannotRequestReviewOnCompleted => new(nameof(CannotRequestReviewOnCompleted), "Cannot request review on completed assignment");
    public static Error InvalidAssignmentStatus(string status) => new(nameof(InvalidAssignmentStatus), "Invalid Assignment Status of: " + status);
}
