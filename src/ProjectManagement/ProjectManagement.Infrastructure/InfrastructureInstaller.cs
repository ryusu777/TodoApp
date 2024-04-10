using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Assignment;
using ProjectManagement.Application.Project;
using ProjectManagement.Application.Subdomain;
using ProjectManagement.Infrastructure.Assignment.Repositories;
using ProjectManagement.Infrastructure.Persistence;
using ProjectManagement.Infrastructure.Persistence.Data;
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
		services.AddDbContext<AppDbContext>(opt =>
		{
			//opt.UseInMemoryDatabase("InMemoryDb");
			opt.UseSqlServer(config.GetConnectionString("AppDbContext"));
		});
		services.AddScoped<IProjectRepository, ProjectRepository>();
		services.AddScoped<IAssignmentRepository, AssignmentRepository>();
		services.AddScoped<ISubdomainRepository, SubdomainRepository>();

		return services;
	}
}
