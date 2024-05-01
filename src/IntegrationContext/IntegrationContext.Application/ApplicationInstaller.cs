using Microsoft.Extensions.DependencyInjection;

namespace IntegrationContext.Application;

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
