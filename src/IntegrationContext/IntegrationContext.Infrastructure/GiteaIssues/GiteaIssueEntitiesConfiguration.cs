using IntegrationContext.Domain.GiteaIssues;
using IntegrationContext.Domain.GiteaIssues.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationContext.Infrastructure.GiteaIssues;

public class GiteaIssueEntitiesConfiguration : IEntityTypeConfiguration<GiteaIssue>
{
    public void Configure(EntityTypeBuilder<GiteaIssue> builder)
    {
        builder.ToTable("GiteaIssue");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GiteaIssueId.Create(value)
            );

        builder.Property(e => e.AssignmentId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AssignmentId.Create(value)
            );

        builder.Property(e => e.GiteaRepositoryId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GiteaRepositoryId.Create(value)
            );

        builder.Property(e => e.IssueNumber)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => IssueNumber.Create(value)
            );

        builder.Property(e => e.UpdatedAt)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.HasOne<GiteaRepository>()
            .WithMany()
            .HasForeignKey(e => e.GiteaRepositoryId);
    }
}

