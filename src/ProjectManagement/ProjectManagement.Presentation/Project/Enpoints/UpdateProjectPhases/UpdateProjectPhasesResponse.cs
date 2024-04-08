using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectPhases;

public record UpdateProjectPhasesResponse(string? ErrorDescription) : IApiResponse;

