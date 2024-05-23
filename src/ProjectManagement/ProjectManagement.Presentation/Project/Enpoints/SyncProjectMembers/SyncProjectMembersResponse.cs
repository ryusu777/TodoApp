using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.SyncProjectMembers;

public record SyncProjectMembersResponse(string? ErrorDescription) : IApiResponse;

