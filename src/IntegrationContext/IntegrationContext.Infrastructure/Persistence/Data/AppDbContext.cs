using System.Reflection;
using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.CommandOutboxes;
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

    protected AppDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    public required virtual DbSet<GiteaUser> GiteaUsers { get; set; }

    public required virtual DbSet<GiteaRepository> GiteaRepositories { get; set; }

    public required virtual DbSet<GiteaIssue> GiteaIssues { get; set; }

    public required virtual DbSet<CommandOutbox> CommandOutboxes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.HasDefaultSchema("integration");
    }
}
