using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.ReopenAssignmentFromHook;

public record ReopenAssignmentFromHookCommand(Guid AssignmentId) : ICommand;
