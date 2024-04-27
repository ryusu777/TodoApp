using System.Reflection;
using AuthContext.Domain.User.Entities;
using AuthContext.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthContext.Infrastructure.Persistence.Data;

public class AppDbContext : IdentityDbContext<AppIdentityUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
    {
    }

    public virtual required DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("auth");

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<AppIdentityUser>(b =>
        {
            b.ToTable("User");
        });

        builder.Entity<IdentityUserClaim<Guid>>(b =>
        {
            b.ToTable("UserClaim");
        });

        builder.Entity<IdentityUserLogin<Guid>>(b =>
        {
            b.ToTable("UserLogin");
        });

        builder.Entity<IdentityUserToken<Guid>>(b =>
        {
            b.ToTable("UserToken");
        });

        builder.Entity<IdentityRole<Guid>>(b =>
        {
            b.ToTable("Role");
        });

        builder.Entity<IdentityRoleClaim<Guid>>(b =>
        {
            b.ToTable("RoleClaim");
        });

        builder.Entity<IdentityUserRole<Guid>>(b =>
        {
            b.ToTable("UserRole");
        });
    }
}
