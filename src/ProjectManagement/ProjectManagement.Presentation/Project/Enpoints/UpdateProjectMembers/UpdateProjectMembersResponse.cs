using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.UpdateProjectMembers;

public record UpdateProjectMembersResponse(string? ErrorDescription) : IApiResponse;

