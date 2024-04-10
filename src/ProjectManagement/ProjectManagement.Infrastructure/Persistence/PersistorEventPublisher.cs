using MediatR;

namespace ProjectManagement.Infrastructure.Persistence;

public class PersistorEventPublisher : INotificationPublisher
{
    public async Task Publish(
        IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, 
        CancellationToken cancellationToken)
    {
        var persistorHandler = handlerExecutors
            .Where(e =>
                typeof(IPersistEventHandler<>)
                    .IsAssignableFrom(e.HandlerInstance.GetType())
            ).ToList();

        foreach (var handler in persistorHandler)
        {
            await handler.HandlerCallback(notification, cancellationToken);
        }
    }
}

