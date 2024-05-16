using System.Reflection;
using MassTransitContracts.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MassTransitContracts.Data;

public class MassTransitDbContext : DbContext
{
    public MassTransitDbContext(DbContextOptions<MassTransitDbContext> options)
        : base(options)
    {
    }

    public required virtual DbSet<OutboxMessage> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("messaging");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
