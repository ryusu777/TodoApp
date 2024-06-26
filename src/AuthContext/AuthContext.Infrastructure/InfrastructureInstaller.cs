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
using AuthContext.Infrastructure.Persistence.Interceptors;
using AuthContext.Infrastructure.Persistence.Mediator;
using AuthContext.Infrastructure.User;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthContext.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config, Type tokenProvider)
    {
		services
		    .AddPersistMediator();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IAuthenticationService, AuthenticationService>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddDbContext<AppDbContext>((s, opt) =>
		{
            var interceptor = s.GetService<AuditableEntityInterceptor>()!;
			//opt.UseInMemoryDatabase("InMemoryDb");
			opt.UseSqlServer(
                config.GetConnectionString("AppDbContext"),
                o => 
                {
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "auth");
                });

            opt.AddInterceptors(interceptor);
		});

        var authenticatorProviderType = typeof(AuthenticatorTokenProvider<>).MakeGenericType(typeof(AppIdentityUser));
        var emailTokenProviderType = typeof(EmailTokenProvider<>).MakeGenericType(typeof(AppIdentityUser));

        services
            .AddIdentityCore<AppIdentityUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddTokenProvider(TokenOptions.DefaultAuthenticatorProvider, authenticatorProviderType)
            .AddTokenProvider(TokenOptions.DefaultEmailProvider, emailTokenProviderType)
            .AddTokenProvider(TokenOptions.DefaultProvider, tokenProvider.MakeGenericType(typeof(AppIdentityUser)));

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
