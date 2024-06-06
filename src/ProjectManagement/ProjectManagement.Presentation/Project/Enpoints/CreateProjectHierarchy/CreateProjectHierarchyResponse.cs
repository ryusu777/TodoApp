using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.CreateProjectHierarchy;

public record CreateProjectHierarchyResponse(
    string? ErrorDescription
) : IApiResponse;
