using MassTransitContracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MassTransitContracts.Data.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessage");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.EventDetail)
            .HasMaxLength(2000)
            .IsRequired(true);

        builder.Property(e => e.ErrorMessage)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(e => e.Tries)
            .HasDefaultValue(0)
            .IsRequired(true);

        builder.Property(e => e.MaxTries)
            .HasDefaultValue(5)
            .IsRequired(true);

        builder.Property(e => e.PublishedAt)
            .IsRequired(false);
    }
}

