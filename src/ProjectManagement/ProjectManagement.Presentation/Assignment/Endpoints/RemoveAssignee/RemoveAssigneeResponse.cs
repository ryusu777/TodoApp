using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.RemoveAssignee;

public record RemoveAssigneeResponse(string? ErrorDescription) : IApiResponse;
