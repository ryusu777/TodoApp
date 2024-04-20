using ProjectManagement.Application.Subdomain.Events;
using ProjectManagement.Domain.Subdomain.Events;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Infrastructure.Subdomain;

public class SubdomainPersistHandler :
    IPersistEventHandler<SubdomainCreated>,
    IPersistEventHandler<SubdomainDeleted>,
    IPersistEventHandler<SubdomainUpdated>,
    IPersistEventHandler<SubdomainKnowledgeCreated>,
    IPersistEventHandler<SubdomainKnowledgeDeleted>,
    IPersistEventHandler<SubdomainKnowledgeUpdated>
{
    public Task Handle(SubdomainKnowledgeUpdated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(SubdomainKnowledgeDeleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(SubdomainKnowledgeCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(SubdomainUpdated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(SubdomainDeleted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.Subdomains.Remove(notification.Subdomain);
        return Task.CompletedTask;
    }

    public Task Handle(SubdomainCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.Subdomains.Add(notification.Subdomain);
        return Task.CompletedTask;
    }
}
