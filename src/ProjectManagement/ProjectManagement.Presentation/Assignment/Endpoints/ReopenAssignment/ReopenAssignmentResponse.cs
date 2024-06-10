using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.ReopenAssignment;

public record ReopenAssignmentResponse(string? ErrorDescription) : IApiResponse;
