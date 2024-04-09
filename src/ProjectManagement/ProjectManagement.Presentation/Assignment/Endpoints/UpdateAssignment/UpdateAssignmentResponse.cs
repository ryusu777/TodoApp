using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.UpdateAssignment;

public record UpdateAssignmentResponse(string? ErrorDescription) : IApiResponse;
