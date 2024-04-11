namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProject;

public record DeleteProjectRequest
{
    public required string ProjectId { get; set; }
}
