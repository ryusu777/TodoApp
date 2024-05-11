using IntegrationContext.Application.GiteaRepositories.Queries.GetProjectRepository;
using IntegrationContext.Presentation.Core.Api;

namespace IntegrationContext.Presentation.GiteaRepositories.Endpoints.GetProjectRepositories;

public record GetProjectRepositoriesResponse(
    string? ErrorCode,
    string? ErrorDescription,
    GetProjectRepositoryResult? Data
) : IApiResponse<GetProjectRepositoryResult>;
