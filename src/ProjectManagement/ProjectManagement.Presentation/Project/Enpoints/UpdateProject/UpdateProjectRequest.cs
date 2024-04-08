using ProjectManagement.Application.Project.Commands.UpdateProjectDetails;
using ProjectManagement.Domain.Project.Enums;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProject;

public record UpdateProjectRequest : UpdateProjectDetailsCommand
{
    public required string id { get; set; }

    public UpdateProjectRequest(
        string ProjectId, 
        string Name, 
        string Description, 
        ProjectStatus Status
    ) : base(ProjectId, Name, Description, Status)
    {
    }
}
