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
		object handlers = _handlerRegistry
			.GetType()
			.GetMethod(nameof(NotificationHandlerRegistry.GetHandlers))!
			.MakeGenericMethod(message.GetType())
			.Invoke(_handlerRegistry, [])!;

		foreach (var handler in (IReadOnlyCollection<object>)handlers)
		{
			var methods = handler
				.GetType()
				.GetMethods()!;

			var method = methods
				.First(e =>
					e.Name == nameof(IPersistEventHandler<TMessage>.Handle) &&
					e.GetParameters().First().ParameterType == message.GetType());

			var task = (Task)method
				.Invoke(handler, [message, dbContext, cancellationToken])!;

			await task.ConfigureAwait(false);
		}
	}

}
