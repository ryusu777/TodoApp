using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProjectManagement.Infrastructure.Persistence.Mediator;

public static class PersistMediatorInstaller
{
	public static IServiceCollection AddPersistMediator(this IServiceCollection services, params Assembly[] assemblies)
	{
		if (assemblies.Length == 0)
		{
			assemblies = new[] { Assembly.GetExecutingAssembly() };
		}

		services.AddScoped<PersistPublisher>();

		foreach (var assembly in assemblies)
		{
			var handlerTypes = assembly
				.GetExportedTypes()
				.Where(e => e
					.GetInterfaces()
					.Any(f => f.IsGenericType && f.GetGenericTypeDefinition() == typeof(IPersistEventHandler<>)))
				.ToList();

			foreach (var handlerType in handlerTypes)
			{
				var interfaces = handlerType.GetInterfaces().Where(e => e.GetGenericTypeDefinition() == typeof(IPersistEventHandler<>)).ToList();
				foreach (var interfaceType in interfaces)
					services.AddTransient(interfaceType, handlerType);
			}
		}

		services.AddSingleton<NotificationHandlerRegistry>();

		return services;
	}
}

