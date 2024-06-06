using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectHierarchyDetails;

public record UpdateProjectHierarchyDetailsResponse(
    string? ErrorDescription
) : IApiResponse;

