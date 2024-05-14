using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues;

public static class GiteaIssueDomainError
{
    public static Error FailedToCreateIssue 
        => new(nameof(FailedToCreateIssue), "Failed to create issue to Gitea");
}
