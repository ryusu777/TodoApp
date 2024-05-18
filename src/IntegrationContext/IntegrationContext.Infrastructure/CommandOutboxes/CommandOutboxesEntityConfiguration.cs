using IntegrationContext.Domain.CommandOutboxes;
using IntegrationContext.Domain.CommandOutboxes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationContext.Infrastructure.CommandOutboxes;

public class CommandOutboxesEntityConfiguration : IEntityTypeConfiguration<CommandOutbox>
{
    public void Configure(EntityTypeBuilder<CommandOutbox> builder)
    {
        builder.ToTable("CommandOutbox");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CommandOutboxId.Create(value)
            );

        builder.Property(e => e.CommandDetail)
            .IsRequired(true)
            .HasColumnType("nvarchar(2000)");

        builder.Property(e => e.CommandResult)
            .IsRequired(false)
            .HasColumnType("nvarchar(2000)");

        builder.Property(e => e.LastError)
            .IsRequired(false)
            .HasColumnType("nvarchar(2000)");
        
        builder.Property(e => e.Tries)
            .HasColumnName("Retries")
            .IsRequired(true);

        builder.Property(e => e.MaxTries)
            .IsRequired(true);

        builder.Property(e => e.SuccessAt)
            .IsRequired(false);

        builder.Property(e => e.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("(sysdatetime())");

        builder.Property(e => e.LastExecutionAt)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("(sysdatetime())");
    }
}

