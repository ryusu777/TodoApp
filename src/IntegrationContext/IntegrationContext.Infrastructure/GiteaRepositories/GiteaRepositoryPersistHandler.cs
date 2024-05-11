using IntegrationContext.Domain.GiteaRepositories.Events;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.GiteaRepositories;

public class GiteaRepositoryPersistHandler
    : IPersistEventHandler<RepositoryHookCreated>
{
    private readonly AppDbContext _context;

    public GiteaRepositoryPersistHandler(AppDbContext context)
    {
        _context = context;
    }

    public Task Handle(RepositoryHookCreated notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        _context.GiteaRepositories.Add(notification.GiteaRepository);
        return Task.CompletedTask;
    }
}

