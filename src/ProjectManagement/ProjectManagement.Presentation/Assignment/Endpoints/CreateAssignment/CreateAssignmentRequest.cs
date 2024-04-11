using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public record CreateAssignmentRequest : CreateAssignmentCommand
{
    public CreateAssignmentRequest(string Title, string Description, string ProjectId) : base(Title, Description, ProjectId)
    {
    }
}

