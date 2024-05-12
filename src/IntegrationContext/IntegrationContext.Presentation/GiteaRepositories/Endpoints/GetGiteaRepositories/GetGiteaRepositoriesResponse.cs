using IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;
using IntegrationContext.Presentation.Core.Api;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.GetGiteaRepositories;

public record GetGiteaRepositoriesResponse(
    string? ErrorCode,
    string? ErrorDescription,
    GetGiteaRepositoryResult? Data
) : IApiResponse<GetGiteaRepositoryResult>;
