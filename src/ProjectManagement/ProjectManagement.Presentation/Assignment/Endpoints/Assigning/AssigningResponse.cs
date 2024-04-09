using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Assignment.Endpoints.Assigning;

public record AssigningResponse(string? ErrorDescription) : IApiResponse;
