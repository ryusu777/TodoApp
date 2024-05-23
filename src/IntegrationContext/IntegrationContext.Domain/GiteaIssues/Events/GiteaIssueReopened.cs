using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.Events;

public record GiteaIssueReopened(GiteaIssue Issue) : IDomainEvent;
