using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.WorkOnAssignment;

public record WorkOnAssignmentCommand(Guid AssignmentId) : ICommand;
