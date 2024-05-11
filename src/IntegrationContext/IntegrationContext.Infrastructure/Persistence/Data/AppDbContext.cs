using System.Reflection;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaRepositories;
using Microsoft.EntityFrameworkCore;

namespace IntegrationContext.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
    {
    }

    public virtual required DbSet<GiteaUser> GiteaUsers { get; set; }

    public virtual required DbSet<GiteaRepository> GiteaRepositories { get; set; }

    public virtual required DbSet<GiteaIssue> GiteaIssues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.HasDefaultSchema("integration");
    }
}
