using IntegrationContext.Domain.GiteaIssues.Events;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.GiteaIssues;

public class GiteaIssuePersistHandler : 
    IPersistEventHandler<GiteaIssueCreated>,
    IPersistEventHandler<GiteaIssueDeleted>
{
    public Task Handle(GiteaIssueCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.GiteaIssues.Add(notification.Issue);
        return Task.CompletedTask;
    }

    public Task Handle(GiteaIssueDeleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.GiteaIssues.Remove(notification.Issue);
        return Task.CompletedTask;
    }
}

