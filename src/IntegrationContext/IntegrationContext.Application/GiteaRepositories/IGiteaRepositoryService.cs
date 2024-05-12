using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Application.Pagination.Models;
using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories.Entities;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaRepositories;

public interface IGiteaRepositoryService
{
    public Task<Result<RepositoryHook>> CreateRepositoryHookAsync(
        JwtToken jwt,
        ProjectId projectId,
        string RepoOwner,
        string RepoName,
        CancellationToken ct
    );

    public Task<Result<GiteaRepositoryDto>> GetGiteaRepositoryByOwnerAsync(
        UserId owner,
        string repoName,
        CancellationToken ct
    );

    public Task<Result<List<GiteaRepositoryDto>>> GetGiteaRepositoriesAsync(
        JwtToken jwt,
        string? searchText,
        Paging? page,
        CancellationToken ct
    );
}
