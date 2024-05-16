using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.Events;

public record GiteaIssueDeleted(GiteaIssue Issue) : IDomainEvent;
