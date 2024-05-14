namespace MassTransitContracts.ProjectManagement.Assignments;

public record AssignmentCreatedMessage(
    string UserId,
    Guid Id,
    string Title, 
    string Description,
    string ProjectId,
    ICollection<string> Assignees,
    DateTime? Deadline,
    int GiteaRepositoryId
);
