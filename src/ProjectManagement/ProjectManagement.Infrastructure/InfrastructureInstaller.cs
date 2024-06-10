using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Application.Assignment.MessageConsumers;
using ProjectManagement.Application.Project;
using ProjectManagement.Application.Subdomain;
using ProjectManagement.Infrastructure.Assignment.Repositories;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;
using ProjectManagement.Infrastructure.Persistence.Interceptors;
using ProjectManagement.Infrastructure.Persistence.Mediator;
using ProjectManagement.Infrastructure.Project.Repositories;
using ProjectManagement.Infrastructure.Subdomain.Repositories;

namespace ProjectManagement.Infrastructure;

public static class InfrastructureInstaller
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		services
			.AddPersistMediator();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddDbContext<AppDbContext>((sp, opt) =>
		{
			//opt.UseInMemoryDatabase("InMemoryDb");
            //
            var auditableIntercepter = sp.GetService<AuditableEntityInterceptor>()!;
			opt
                .UseSqlServer(config.GetConnectionString("AppDbContext"))
                .AddInterceptors(auditableIntercepter);
		});
		services.AddScoped<IProjectRepository, ProjectRepository>();
		services.AddScoped<IAssignmentRepository, AssignmentRepository>();
		services.AddScoped<ISubdomainRepository, SubdomainRepository>();

        services.AddMassTransit(bc => 
        {
            bc.SetKebabCaseEndpointNameFormatter();
            bc.AddConsumer<IssueCreatedMessageConsumer>();
            bc.AddConsumer<IssueUpdatedMessageConsumer>();
            bc.AddConsumer<IssueClosedMessageConsumer>();
            bc.AddConsumer<IssueReopenedMessageConsumer>();
            bc.UsingRabbitMq((context, configurator) => 
            {
                configurator.Host(new Uri(config["MessageBroker:Host"]!), h =>
                {
                    h.Username(config["MessageBroker:Username"]!);
                    h.Username(config["MessageBroker:Password"]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

		return services;
	}
}
