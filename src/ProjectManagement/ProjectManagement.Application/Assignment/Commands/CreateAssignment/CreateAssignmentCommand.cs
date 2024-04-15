using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignment;

public record CreateAssignmentCommand(
	string Title, 
	string Description, 
	string ProjectId,
    Guid SubdomainId,
    Guid PhaseId
) : ICommand;
