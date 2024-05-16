using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Application.GiteaRepositories.Dtos;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using IntegrationContext.Infrastructure.Persistence.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationContext.Infrastructure.GiteaRepositories;

public class GiteaRepositoryRepository : IGiteaRepositoryRepository
{
    private readonly AppDbContext _dbContext;

    public GiteaRepositoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GiteaRepositoryDto>>> GetProjectRepositoriesAsync(ProjectId projectId, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaRepositories
            .Where(e => e.ProjectId == projectId)
            .Select(e => new GiteaRepositoryDto(
                e.Id.Value, 
                e.RepoOwner.Value, 
                e.RepoName))
            .ToListAsync();

        return Result.Success(result);
    }

    public async Task<Result<GiteaRepositoryDto>> GetProjectRepositoryByIdAsync(GiteaRepositoryId id, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaRepositories
            .Where(e => e.Id == id)
            .Select(e => new GiteaRepositoryDto(
                e.Id.Value, 
                e.RepoOwner.Value, 
                e.RepoName))
            .FirstOrDefaultAsync();

        if (result is null)
            return Result
                .Failure<GiteaRepositoryDto>(GiteaRepositoryDomainError
                    .GiteaRepositoryNotFoundInTheProject);

        return Result.Success(result);
    }
}

