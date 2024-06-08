using ProjectManagement.Application.Project.Queries.GetAssignableHierarchies;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.GetAssignableHierarchies;

public record GetAssignableHierarchiesResponse(
    string? ErrorDescription,
    GetAssignableHierarchiesResult? Data
) : IApiResponse<GetAssignableHierarchiesResult>;
