using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProject;

public record UpdateProjectResponse(string? ErrorDescription) : IApiResponse;

