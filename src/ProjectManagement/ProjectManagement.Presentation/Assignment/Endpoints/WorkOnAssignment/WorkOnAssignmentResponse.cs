using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Project.Endpoints.WorkOnAssignment;

public record WorkOnAssignmentResponse(string? ErrorDescription) : IApiResponse;
