using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransitContracts.Data;

public static class MassTransitDbContextMigrator
{
    public static async Task<IServiceCollection> Migrate(this IServiceCollection services, CancellationToken ct = default)
    {
        using var serviceProvider = services.BuildServiceProvider();

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MassTransitDbContext>();
            
            await dbContext.Database.MigrateAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error while migrating database for context AppDbContext", ex);
        }
        return services;
    }
}
