using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using Library.Models;
using MassTransitContracts.ProjectManagement.Assignments;

namespace IntegrationContext.Application.GiteaIssues;

public interface IGiteaIssueApiService
{
    public Task<Result<GiteaIssue>> CreateIssueAsync(
        JwtToken jwt, AssignmentCreatedMessage message, CancellationToken ct);

    public Task<Result<string>> UpdateIssueAsync(
        JwtToken jwt, 
        AssignmentUpdatedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct);

    public Task<Result> DeleteIssueAsync(
        JwtToken jwt, 
        AssignmentDeletedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct);

    public Task<Result<string>> CloseIssueAsync(
        JwtToken jwt, 
        AssignmentCompletedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct);

    public Task<Result<string>> ReopenIssueAsync(
        JwtToken jwt, 
        AssignmentRenewedMessage message, 
        UserId repoOwner,
        string repoName,
        IssueNumber issueNumber,
        CancellationToken ct);
}
