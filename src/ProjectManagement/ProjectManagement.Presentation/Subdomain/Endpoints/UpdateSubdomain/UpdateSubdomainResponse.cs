using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.UpdateSubdomain;

public record UpdateSubdomainResponse(string? ErrorDescription) : IApiResponse;
