using ProjectManagement.Application.Project.Queries.GetAllProjectMembers;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.GetAllProjectMembers;

public record GetAllProjectMembersResponse(
    string? ErrorDescription,
    GetAllProjectMembersResult? Data
) : IApiResponse<GetAllProjectMembersResult>;
