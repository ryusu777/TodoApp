namespace MassTransitContracts.ProjectManagement.Assignments;

public record AssignmentDeletedMessage(
    Guid Id,
    string UserId
);
