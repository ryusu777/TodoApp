using IntegrationContext.Application.GiteaIssues;
using IntegrationContext.Application.GiteaIssues.Dtos;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Infrastructure.Persistence.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IntegrationContext.Infrastructure.GiteaIssues;

public class GiteaIssueRepository : IGiteaIssueRepository
{
    private readonly AppDbContext _dbContext;
    private readonly string _giteaUri;

    public GiteaIssueRepository(AppDbContext dbContext, IConfiguration config)
    {
        _dbContext = dbContext;
        _giteaUri = config["GiteaUrl"]!;
        Console.WriteLine("===========" + _giteaUri);
    }

    private Uri GenerateIssueUrl(string repoOwner, string repoName, int issueNumber)
    {
        Uri giteaUri = new Uri(_giteaUri);
        UriBuilder builder = new UriBuilder();
        builder.Host = giteaUri.Host;
        builder.Port = giteaUri.Port;
        builder.Scheme = giteaUri.Scheme;

        builder.Path = $"/{repoOwner}/{repoName}/issues/{issueNumber}";

        return builder.Uri;
    }

    public async Task<Result<List<AssignmentIssueNumber>>> GetAssignmentsIssueNumberAsync(ICollection<AssignmentId> assignmentIds, CancellationToken ct)
    {
        var result = await _dbContext
            .GiteaIssues
            .Where(e => assignmentIds.Contains(e.AssignmentId))
            .Join(_dbContext.GiteaRepositories,
                (issue) => issue.GiteaRepositoryId,
                (repository) => repository.Id,
                (issue, repository) => new { issue.AssignmentId, issue.IssueNumber, repository.RepoName, repository.RepoOwner }
            )
            .ToListAsync(ct);

        if (result is null)
            return Result.Success(new List<AssignmentIssueNumber>());

        return Result.Success(result
            .Select(e => new AssignmentIssueNumber(
                e.AssignmentId.Value, e.IssueNumber.Value,
                GenerateIssueUrl(
                    e.RepoOwner.Value, 
                    e.RepoName, 
                    e.IssueNumber.Value
                )))
            .ToList());
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

