using IntegrationContext.Application.GiteaRepositories.Dtos;

namespace IntegrationContext.Application.GiteaRepositories.Queries.GetGiteaRepository;

public record GetGiteaRepositoryResult(
    ICollection<GiteaRepositoryDto> Repositories
);
