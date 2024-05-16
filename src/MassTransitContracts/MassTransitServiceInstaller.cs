using System.Reflection;
using MassTransitContracts.Data;
using MassTransitContracts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitContracts;

public static class MassTransitServiceInstaller
{
    public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration config)
    {
		services.AddDbContext<MassTransitDbContext>(opt =>
		{
			//opt.UseInMemoryDatabase("InMemoryDb");
			opt.UseSqlServer(
                config.GetConnectionString("MassTransitDbContext"),
                o => 
                {
                    o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "messaging");
                });
		});
        services.AddScoped<IMassTransitService, MassTransitService>();
        return services;
    }
}
