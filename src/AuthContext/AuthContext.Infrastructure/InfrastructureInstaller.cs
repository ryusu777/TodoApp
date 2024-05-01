using AuthContext.Application.Abstractions.Data;
using AuthContext.Application.Email;
using AuthContext.Application.Identity;
using AuthContext.Application.Identity.Model;
using AuthContext.Application.User;
using AuthContext.Infrastructure.Email;
using AuthContext.Infrastructure.Identity;
using AuthContext.Infrastructure.Identity.Entities;
using AuthContext.Infrastructure.Persistence;
using AuthContext.Infrastructure.Persistence.Data;
using AuthContext.Infrastructure.Persistence.Mediator;
using AuthContext.Infrastructure.User;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthContext.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
		services
		    .AddPersistMediator();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IAuthenticationService, AuthenticationService>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddDbContext<AppDbContext>(opt =>
		{
			//opt.UseInMemoryDatabase("InMemoryDb");
			opt.UseSqlServer(
                config.GetConnectionString("AppDbContext"),
                o => 
                {
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "auth");
                });
		});

        services
            .AddIdentityCore<AppIdentityUser>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddMassTransit(bc => 
        {
            bc.SetKebabCaseEndpointNameFormatter();
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

        services.Configure<JwtOptions>(config.GetSection(JwtOptions.OptionSection));

        return services;
    }
}
