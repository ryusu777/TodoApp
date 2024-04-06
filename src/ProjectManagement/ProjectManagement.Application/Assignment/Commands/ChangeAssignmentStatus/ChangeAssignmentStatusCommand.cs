using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.ChangeAssignmentStatus;

public record ChangeAssignmentStatusCommand(
    Guid AssignmentId,
    string AssignmentStatus
) : ICommand;

