using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.Events;

public record GiteaIssueCreated(GiteaIssue Issue) : IDomainEvent;
