using AuthContext.Domain.User.ValueObjects;
using AuthContext.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthContext.Infrastructure.Identity;

public class IdentityEntityConfiguration : IEntityTypeConfiguration<AppIdentityUser>
{
    public void Configure(EntityTypeBuilder<AppIdentityUser> builder)
    {
        ConfigureRefreshTokenTable(builder);
    }

    private void ConfigureRefreshTokenTable(EntityTypeBuilder<AppIdentityUser> builder)
    {
        builder.OwnsMany(e => e.RefreshToken, rb => 
        {
            rb.HasKey(e => e.Id);

            rb.WithOwner();

            rb.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => Jti.Create(value)
                );

            rb.Property(e => e.RefreshToken)
                .HasColumnType("varchar(250)")
                .IsRequired(true)
                .HasConversion(
                    id => id.Value,
                    value => RefreshToken.Create(value)
                );

            rb.Property(e => e.ExpiresAt)
                .IsRequired(true);
        });
    }
}
