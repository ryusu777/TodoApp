namespace MassTransitContracts.ProjectManagement.Assignments;

public record AssignmentUpdatedMessage(
    string UserId,
    Guid Id,
    string Title, 
    string? Description,
    ICollection<string> Assignees,
    DateTime? Deadline
);
