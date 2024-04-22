using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.CreateAssignment;

public record CreateAssignmentCommand(
	string Title, 
	string Description, 
	string ProjectId,
    DateTime? Deadline,
    string? Reviewer = null,
    Guid? SubdomainId = null,
    Guid? PhaseId = null
) : ICommand;
