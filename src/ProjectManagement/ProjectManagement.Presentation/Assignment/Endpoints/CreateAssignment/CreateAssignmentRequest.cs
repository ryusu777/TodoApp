using ProjectManagement.Application.Assignment.Commands.CreateAssignment;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public record CreateAssignmentRequest : CreateAssignmentCommand
{
    public required string project_id { get; set; }
    public CreateAssignmentRequest(string Title, string Description, string ProjectId) : base(Title, Description, ProjectId)
    {
    }
}

