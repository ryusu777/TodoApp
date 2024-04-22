using ProjectManagement.Application.Assignment.Commands.UpdateAssignments;

namespace ProjectManagement.Presentation.Assignment.Endpoints.UpdateAssignment;

public record UpdateAssignmentRequest : UpdateAssignmentCommand
{
    public UpdateAssignmentRequest(
        Guid AssignmentId, 
        string Title, 
        string Description, 
        Guid? SubdomainId = null, 
        Guid? PhaseId = null, 
        string? Reviewer = null) : base(AssignmentId, Title, Description, SubdomainId, PhaseId, Reviewer)
    {
    }
}

