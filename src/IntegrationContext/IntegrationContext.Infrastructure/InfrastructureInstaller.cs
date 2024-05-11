using IntegrationContext.Application.Abstractions.Data;
using IntegrationContext.Application.Auth;
using IntegrationContext.Application.Auth.Messaging.GetAuthProviderUri;
using IntegrationContext.Application.Auth.Models;
using IntegrationContext.Application.GiteaRepositories;
using IntegrationContext.Infrastructure.Auth;
using IntegrationContext.Infrastructure.GiteaRepositories.ApiService;
using IntegrationContext.Infrastructure.GiteaUsers;
using IntegrationContext.Infrastructure.Persistence;
using IntegrationContext.Infrastructure.Persistence.Data;
using IntegrationContext.Infrastructure.Persistence.Mediator;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationContext.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services
            .AddPersistMediator();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGiteaAuthenticationService, GiteaAuthenticationService>();
        services.AddScoped<IGiteaRepositoryService, GiteaRepositoryService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<AppDbContext>(opt =>
        {
            //opt.UseInMemoryDatabase("InMemoryDb");
            opt.UseSqlServer(
                config.GetConnectionString("AppDbContext"),
                o => 
                {
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "integration");
                });
        });

        services.AddMassTransit(bc => 
        {
            bc.SetKebabCaseEndpointNameFormatter();
            bc.AddConsumer<GetAuthProviderUriConsumer>();
            bc.AddConsumer<GrantAccessTokenConsumer>();
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

        services.AddHttpClient(GiteaAuthenticationService.CLIENT_NAME, (serviceProvider, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(config["GiteaUrl"]! + "/api/v1");
        });

        services.AddHttpContextAccessor();

        services.Configure<GiteaClientCredentials>(config.GetSection(GiteaClientCredentials.OptionSection));

        return services;
    }
}
