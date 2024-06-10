using ProjectManagement.Application.Project.Queries.GetProjectDashboard;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectDashboard;

public record GetProjectDashboardResponse(
    string? ErrorDescription,
    GetProjectDashboardResult? Data
) : IApiResponse<GetProjectDashboardResult>;
