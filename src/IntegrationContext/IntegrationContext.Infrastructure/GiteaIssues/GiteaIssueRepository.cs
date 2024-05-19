using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Infrastructure.Persistence.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace IntegrationContext.Infrastructure.GiteaIssues;

public class GiteaIssueRepository : IGiteaIssueRepository
{
    private readonly AppDbContext _dbContext;

    public GiteaIssueRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GiteaIssue>> GetIssueByAssignmentIdAsync(AssignmentId id, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaIssues
            .FirstOrDefaultAsync(e => e.AssignmentId == id, ct);

        if (result is null)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.IssueNotFound);

        return Result.Success(result);
    }

    public async Task<Result<GiteaIssue>> GetIssueByIdAsync(GiteaIssueId id, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaIssues
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (result is null)
            return Result.Failure<GiteaIssue>(GiteaIssueDomainError.IssueNotFound);

        return Result.Success(result);
    }

    public async Task<bool> IssueExistsAsync(GiteaIssueId id, CancellationToken ct)
    {
        return await _dbContext
            .GiteaIssues
            .AnyAsync(e => e.Id == id, ct);
    }
}

