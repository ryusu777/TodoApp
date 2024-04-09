using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.DeleteAssignment;

public record DeleteAssignmentResponse(string? ErrorDescription) : IApiResponse;
