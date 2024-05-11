using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories;

public interface IGiteaRepositoryService
{
    public Task<Result<int>> CreateRepositoryHookAsync(
        ProjectId projectId,
        string RepoOwner,
        string RepoName,
        CancellationToken ct
    );

    public Task<Result<List<GiteaRepositoryDto>>> GetGiteaRepositoriesAsync(
        string searchText,
        Paging? page,
        CancellationToken ct
    );
}
