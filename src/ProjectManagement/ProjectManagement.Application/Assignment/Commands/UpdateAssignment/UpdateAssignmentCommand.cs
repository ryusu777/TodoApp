using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

public record UpdateAssignmentCommand(
    Guid AssignmentId, 
    string Title, 
    string Description,
    Guid? SubdomainId = null,
    Guid? PhaseId = null,
    string? Reviewer = null
) : ICommand;
