using IntegrationContext.Domain.Auth.ValueObjects;
using IntegrationContext.Domain.GiteaRepositories;
using IntegrationContext.Domain.GiteaRepositories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntegrationContext.Infrastructure.Gitea;

public class GiteaRepositoryEntitiesConfiguration : IEntityTypeConfiguration<GiteaRepository>
{
    public void Configure(EntityTypeBuilder<GiteaRepository> builder)
    {
        builder.ToTable("GiteaRepository");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GiteaRepositoryId.Create(value)
            );

        builder.Property(e => e.RepoOwner)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );

        builder.Property(e => e.RepoName)
            .IsRequired(true)
            .HasMaxLength(100)
            .ValueGeneratedNever();

        builder.Property(e => e.ProjectId)
            .IsRequired(true)
            .HasMaxLength(30)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProjectId.Create(value)
            );
    }
}
