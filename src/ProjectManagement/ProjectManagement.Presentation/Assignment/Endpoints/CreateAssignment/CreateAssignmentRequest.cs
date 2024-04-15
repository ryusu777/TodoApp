using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public record CreateAssignmentRequest : CreateAssignmentCommand
{
    public CreateAssignmentRequest(
        string Title, 
        string Description, 
        string ProjectId, 
        Guid SubdomainId, 
        Guid PhaseId) : base(Title, Description, ProjectId, SubdomainId, PhaseId)
    {
    }
}

