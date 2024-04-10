using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManagement.Infrastructure.Persistence.Mediator;

public class NotificationHandlerRegistry
{
	private readonly IServiceProvider _serviceProvider;

	public NotificationHandlerRegistry(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public IReadOnlyCollection<IPersistEventHandler<T>> GetHandlers<T>() where T : INotification
	{
		//Type handlerType = typeof(IPersistEventHandler<>).MakeGenericType(messageType);

		IServiceScope scopedProvider = _serviceProvider.CreateScope();

		var result = scopedProvider.ServiceProvider.GetServices<IPersistEventHandler<T>>().ToList();

		return result;
	}
}