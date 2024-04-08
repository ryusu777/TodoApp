using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProject;

public record CreateProjectResponse(string? ErrorDescription) : IApiResponse;

