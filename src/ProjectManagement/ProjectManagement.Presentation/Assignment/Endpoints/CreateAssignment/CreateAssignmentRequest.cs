using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public record CreateAssignmentRequest : CreateAssignmentCommand
{
    public CreateAssignmentRequest(
        string Title, 
        string Description, 
        string ProjectId, 
        DateTime? Deadline, 
        string? Reviewer = null, 
        Guid? SubdomainId = null, 
        Guid? PhaseId = null) : base(Title, Description, ProjectId, Deadline, Reviewer, SubdomainId, PhaseId)
    {
    }
}

