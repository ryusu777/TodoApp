namespace MassTransitContracts.Hooks.Issue;

public record IssueUpdatedMessage(
    Guid AssignmentId,
    string Title,
    string Body,
    ICollection<string> Assignees,
    DateTime? DueDate
);
