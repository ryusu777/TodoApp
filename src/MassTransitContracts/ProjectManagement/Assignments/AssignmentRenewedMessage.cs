namespace MassTransitContracts.ProjectManagement.Assignments;

public record AssignmentRenewedMessage(
    Guid AssignmentId,
    string UserId
);
