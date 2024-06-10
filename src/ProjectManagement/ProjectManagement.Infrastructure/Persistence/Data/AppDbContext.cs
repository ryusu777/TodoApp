using Library.Models;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Project.Entities;
using ProjectManagement.Domain.Subdomain.Entities;
using System.Reflection;

namespace ProjectManagement.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public required virtual DbSet<Domain.Project.Project> Projects { get; set; }
    public required virtual DbSet<Phase> ProjectPhases { get; set; }
    public required virtual DbSet<Domain.Assignment.Assignment> Assignments { get; set; }
    public required virtual DbSet<Domain.Subdomain.Subdomain> Subdomains { get; set; }
    public required virtual DbSet<SubdomainKnowledge> SubdomainKnowledges { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
