namespace MassTransitContracts.Hooks.Issue;

public record IssueCreatedMessage(
    Guid AssignmentId,
    string ProjectId,
    string Title,
    string Body,
    ICollection<string> Assignees,
    DateTime? DueDate
);
