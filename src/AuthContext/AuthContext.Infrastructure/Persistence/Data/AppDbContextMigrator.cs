using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthContext.Infrastructure.Persistence.Data;

public static class AppDbContextMigrator
{
    public static async Task<IServiceCollection> Migrate(this IServiceCollection services, CancellationToken ct = default)
    {
        using var serviceProvider = services.BuildServiceProvider();

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            await dbContext.Database.MigrateAsync(ct);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error while migrating database for context AppDbContext", ex);
        }
        return services;
    }
}
