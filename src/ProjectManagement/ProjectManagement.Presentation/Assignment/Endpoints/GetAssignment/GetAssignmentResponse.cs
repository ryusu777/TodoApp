using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.GetAssignment;

public record GetAssignmentResponse(
    string? ErrorDescription,
    Application.Assignment.Dtos.Assignment? Data
) : IApiResponse<Application.Assignment.Dtos.Assignment>;
