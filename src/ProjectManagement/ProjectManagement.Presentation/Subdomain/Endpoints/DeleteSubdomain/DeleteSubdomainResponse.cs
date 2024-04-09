using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.DeleteSubdomain;

public record DeleteSubdomainResponse(string? ErrorDescription) : IApiResponse;
