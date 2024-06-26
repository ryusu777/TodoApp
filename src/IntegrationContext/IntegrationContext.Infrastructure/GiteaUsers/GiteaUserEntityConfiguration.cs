using IntegrationContext.Domain.Auth;
using IntegrationContext.Domain.Auth.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationContext.Infrastructure.GiteaUsers;

public class GiteaUserEntitiesConfiguration : IEntityTypeConfiguration<GiteaUser>
{
    public void Configure(EntityTypeBuilder<GiteaUser> builder)
    {
        builder.ToTable("GiteaUser");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GiteaUserId.Create(value)
            );

        builder.Property(e => e.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );

        builder.Property(e => e.JwtToken)
            .IsRequired(false)
            .ValueGeneratedNever()
            .HasConversion(
                token => token!.Value,
                value => JwtToken.Create(value)
            );

        builder.Property(e => e.RefreshToken)
            .IsRequired(false)
            .ValueGeneratedNever()
            .HasConversion(
                token => token!.Value,
                value => RefreshToken.Create(value)
            );

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ModifiedBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.ModifiedAt)
            .IsRequired(false);
    }
}
