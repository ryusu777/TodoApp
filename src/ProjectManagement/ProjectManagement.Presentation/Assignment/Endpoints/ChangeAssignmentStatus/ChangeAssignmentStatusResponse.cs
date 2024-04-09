using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.ChangeAssignmentStatus;

public record ChangeAssignmentStatusResponse(string? ErrorDescription) : IApiResponse;
