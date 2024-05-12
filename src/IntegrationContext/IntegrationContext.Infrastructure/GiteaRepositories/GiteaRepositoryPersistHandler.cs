using IntegrationContext.Domain.GiteaRepositories.Events;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.GiteaRepositories;

public class GiteaRepositoryPersistHandler
    : IPersistEventHandler<GiteaRepositoryCreated>
{
    public Task Handle(GiteaRepositoryCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.GiteaRepositories.Add(notification.GiteaRepository);
        return Task.CompletedTask;
    }
}

