using ProjectManagement.Application.Subdomain.Queries.GetSubdomain;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.GetSubdomain;

public record GetSubdomainResponse(
    string? ErrorDescription,
    GetSubdomainResult? Data
) : IApiResponse<GetSubdomainResult>;
