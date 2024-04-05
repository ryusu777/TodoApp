using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Behaviours;

namespace ProjectManagement.Application;

public static class ApplicationInstaller
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
		});

		return services;
	}
}
