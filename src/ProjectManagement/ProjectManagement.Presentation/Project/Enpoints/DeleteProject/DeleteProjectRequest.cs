using ProjectManagement.Application.Project.Commands.DeleteProject;

namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProject;

public record DeleteProjectRequest : DeleteProjectCommand
{
    public string id { get; set; }
    public DeleteProjectRequest(string ProjectId, string id) : base(ProjectId)
    {
        this.id = id;
    }
}
