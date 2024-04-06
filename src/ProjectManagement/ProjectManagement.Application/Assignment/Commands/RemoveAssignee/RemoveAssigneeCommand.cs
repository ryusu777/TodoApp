using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands;

public record RemoveAssigneeCommand(
    Guid AssignmentId, 
    string AssigneeUsername) : ICommand;
