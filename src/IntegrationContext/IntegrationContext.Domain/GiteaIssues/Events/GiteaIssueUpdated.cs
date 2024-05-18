using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.Events;

public record GiteaIssueUpdated(GiteaIssue Issue) : IDomainEvent;
