using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using Library.Models;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues;

public interface IGiteaIssueApiService
{
    public Task<Result<GiteaIssue>> CreateIssueAsync(
        JwtToken jwt, AssignmentCreatedMessage message, CancellationToken ct);

    public Task<Result> UpdateIssueAsync(
        JwtToken jwt, AssignmentUpdatedMessage message, CancellationToken ct);

    public Task<Result<GiteaIssue>> DeleteIssueAsync(
        JwtToken jwt, AssignmentDeletedMessage message, CancellationToken ct);
}
