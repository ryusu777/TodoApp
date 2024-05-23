using Library.Models;

namespace IntegrationContext.Domain.GiteaIssues.Events;

public record GiteaIssueClosed(GiteaIssue Issue) : IDomainEvent;
