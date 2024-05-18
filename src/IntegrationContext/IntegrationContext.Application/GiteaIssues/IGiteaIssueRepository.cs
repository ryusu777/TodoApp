using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;

namespace IntegrationContext.Application.GiteaIssues;

public interface IGiteaIssueRepository
{
    public Task<Result<GiteaIssue>> GetIssueByAssignmentId(AssignmentId id, CancellationToken ct);
    public Task<bool> IssueExistsAsync(GiteaIssueId id, CancellationToken ct);
}
