using IntegrationContext.Domain.CommandOutboxes.Events;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;

namespace IntegrationContext.Infrastructure.CommandOutboxes;

public class CommandOutboxPersistHandler
    : IPersistEventHandler<CommandPersisted>
{
    public Task Handle(CommandPersisted notification, AppDbContext dbContext, CancellationToken cancellationToken)
    {
        dbContext.CommandOutboxes.Add(notification.Command);

        return Task.CompletedTask;
    }
}

