using ProjectManagement.Application.Abstractions.Messaging;
namespace ProjectManagement.Application.Assignment.Commands.Assigning;

public record AssigningCommand(Guid AssignmentId, string AssigneeUsername) : ICommand;
