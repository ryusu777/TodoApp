using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.CreateAssignment;

public record CreateAssignmentResponse(string? ErrorDescription) : IApiResponse;

