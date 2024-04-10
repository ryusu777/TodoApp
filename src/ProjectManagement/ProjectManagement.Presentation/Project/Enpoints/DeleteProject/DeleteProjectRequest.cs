namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProject;

public record DeleteProjectRequest
{
    public required string id { get; set; }
}
