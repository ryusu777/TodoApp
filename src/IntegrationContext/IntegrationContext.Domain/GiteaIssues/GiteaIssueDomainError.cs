using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues;

public static class GiteaIssueDomainError
{
    public static Error FailedToCreateIssue 
        => new(nameof(FailedToCreateIssue), "Failed to create issue to Gitea");

    public static Error FailedToUpdateIssue 
        => new(nameof(FailedToUpdateIssue), "Failed to update issue to Gitea");

    public static Error FailedToDeleteIssue(string message)
        => new(nameof(FailedToDeleteIssue), message);

    public static Error FailedToCloseIssue(string message)
        => new(nameof(FailedToCloseIssue), message);

    public static Error IssueNotFound 
        => new(nameof(IssueNotFound), "The issue is not found in this application");
}
