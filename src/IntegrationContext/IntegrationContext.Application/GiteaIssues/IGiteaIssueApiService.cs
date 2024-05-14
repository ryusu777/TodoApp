using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using Library.Models;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues;

public interface IGiteaIssueApiService
{
    public Task<Result<GiteaIssue>> CreateIssueAsync(
        JwtToken jwt, AssignmentCreatedMessage message, CancellationToken ct);
}
