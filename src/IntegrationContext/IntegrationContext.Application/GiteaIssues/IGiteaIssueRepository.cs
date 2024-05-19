using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaIssues;

public interface IGiteaIssueRepository
{
    public Task<Result<GiteaIssue>> GetIssueByAssignmentIdAsync(AssignmentId id, CancellationToken ct);
    public Task<Result<GiteaIssue>> GetIssueByIdAsync(GiteaIssueId id, CancellationToken ct);
    public Task<bool> IssueExistsAsync(GiteaIssueId id, CancellationToken ct);
}
