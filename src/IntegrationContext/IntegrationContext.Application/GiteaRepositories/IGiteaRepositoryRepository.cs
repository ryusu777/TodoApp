using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories;

public interface IGiteaRepositoryRepository
{
    public Task<Result<List<GiteaRepositoryDto>>> GetProjectRepositoriesAsync(
        ProjectId projectId,
        CancellationToken ct
    );

    public Task<Result<GiteaRepositoryDto>> GetProjectRepositoryByIdAsync(
        GiteaRepositoryId id,
        ProjectId projectId,
        CancellationToken ct
    );
}
