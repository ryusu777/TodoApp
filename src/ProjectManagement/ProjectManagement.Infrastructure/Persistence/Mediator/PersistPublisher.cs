using MediatR;
using ProjectManagement.Infrastructure.Persistence.Data;
using System.Reflection;

namespace ProjectManagement.Infrastructure.Persistence.Mediator;

public class PersistPublisher
{
	private readonly NotificationHandlerRegistry _handlerRegistry;

	public PersistPublisher(NotificationHandlerRegistry handlerRegistry)
	{
		_handlerRegistry = handlerRegistry;
	}

	public async Task Publish<TMessage>(TMessage message, AppDbContext dbContext, CancellationToken cancellationToken)
		where TMessage : INotification
	{
        foreach (var handler in _handlerRegistry.GetHandlers<TMessage>())
        {
            await handler.Handle(message, dbContext, cancellationToken);
        }
	}

}
