using System.Reflection;
using IntegrationContext.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace IntegrationContext.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
    {
    }

    public virtual required DbSet<GiteaUser> GiteaUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.HasDefaultSchema("integration");
    }
}
