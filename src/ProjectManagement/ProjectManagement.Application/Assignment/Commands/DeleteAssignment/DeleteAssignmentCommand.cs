using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.DeleteAssignment;

public record DeleteAssignmentCommand(Guid AssignmentId) : ICommand;
