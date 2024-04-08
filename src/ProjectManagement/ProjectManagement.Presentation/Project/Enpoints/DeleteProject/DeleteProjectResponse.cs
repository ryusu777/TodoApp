using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProject;

public record DeleteProjectResponse(string? ErrorDescription) : IApiResponse;

