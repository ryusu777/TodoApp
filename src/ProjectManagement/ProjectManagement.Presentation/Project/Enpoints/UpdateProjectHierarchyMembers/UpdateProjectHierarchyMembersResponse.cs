using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectHierarchyMembers;

public record UpdateProjectHierarchyMembersResponse(
    string? ErrorDescription
) : IApiResponse;
