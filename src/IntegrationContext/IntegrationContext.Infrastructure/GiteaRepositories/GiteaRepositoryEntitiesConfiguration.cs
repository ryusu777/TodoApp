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
        ConfigureRepositoryTable(builder);
        ConfigureHookTable(builder);
    }

    private void ConfigureRepositoryTable(EntityTypeBuilder<GiteaRepository> builder)
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
    
    private void ConfigureHookTable(EntityTypeBuilder<GiteaRepository> builder)
    {
        builder.OwnsMany(e => e.Hooks, hb => {
            hb.ToTable("GiteaRepositoryHook");

            hb.WithOwner().HasForeignKey("GiteaRepositoryId");

            hb.HasKey(e => e.Id);

            hb.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RepositoryHookId.Create(value));

            hb.Property(e => e.HookUri)
                .IsRequired()
                .HasMaxLength(100);

            hb.Property(e => e.Events)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasConversion(
                    events => events.Aggregate("",
                        (total, next) => total + "," + next.Value),
                    value => SplitHook(value)
                );

            hb.Property(e => e.Active)
                .HasDefaultValue(true);

            hb.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            hb.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);

            hb.Property(e => e.CreatedAt)
                .IsRequired();

            hb.Property(e => e.ModifiedAt)
                .IsRequired(false);
        });
    }

    private ICollection<HookEvent> SplitHook(string val)
    {
        return val.Split(",").Select(e => HookEvent.Create(e)).ToList();
    }
}
