using ProjectManagement.Application.Assignment.Queries.GetAssignments;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.GetAssignments;

public record GetAssignmentsResponse(
    string? ErrorDescription,
    GetAssignmentsResult? Data
) : IApiResponse<GetAssignmentsResult>;
