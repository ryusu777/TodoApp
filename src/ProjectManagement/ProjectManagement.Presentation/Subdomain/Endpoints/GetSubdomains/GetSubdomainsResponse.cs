using ProjectManagement.Application.Subdomain.Queries.GetSubdomains;
using ProjectManagement.Presentation.Core.Api;

namespace ProjectManagement.Presentation.Subdomain.Endpoints.GetSubdomains;

public record GetSubdomainsResponse(
    string? ErrorDescription,
    GetSubdomainsResult? Data
) : IApiResponse<GetSubdomainsResult>;
