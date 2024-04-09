using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.CreateSubdomain;

public record CreateSubdomainResponse(string? ErrorDescription) : IApiResponse;
