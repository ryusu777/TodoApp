using ProjectManagement.Application.Project.Queries.GetProjectById;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.GetProjectById;

public record GetProjectByIdResponse(
    GetProjectByIdResult? Data,
    string? ErrorDescription
) : IApiResponse<GetProjectByIdResult>;
