using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.DeleteProjectHierarchy;

public record DeleteProjectHierarchyResponse(
    string? ErrorDescription
) : IApiResponse;

