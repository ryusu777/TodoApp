using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.ReopenAssignment;

public record ReopenAssignmentCommand(
    string UserId,
    Guid AssignmentId, 
    string NewDescription
) : ICommand;
