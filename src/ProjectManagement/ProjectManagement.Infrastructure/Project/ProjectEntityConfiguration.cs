using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Infrastructure.Project;

public class ProjectEntityConfiguration : IEntityTypeConfiguration<Domain.Project.Project>
{
    public void Configure(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        ConfigureProjectTable(builder);
        ConfigureProjectPhaseTable(builder);
        ConfigureProjectMemberTable(builder);
        ConfigureProjectHierarchyTable(builder);
    }

    private void ConfigureProjectTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasMaxLength(30)
            .HasConversion(
                id => id.Value,
                value => ProjectId.Create(value));

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Status)
            .IsRequired()
            .HasColumnType("varchar(10)");

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.ModifiedAt)
            .IsRequired(false);

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ModifiedBy)
            .IsRequired(false)
            .HasMaxLength(50);
    }

    private void ConfigureProjectPhaseTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder
            .OwnsMany(e => e.ProjectPhases, sb =>
            {
                sb.ToTable("ProjectPhase");

                sb.HasKey(e => e.Id);

                sb.WithOwner().HasForeignKey("ProjectId");

				sb.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasConversion(
						id => id.Value,
						value => PhaseId.Create(value));

                sb.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                sb.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                sb.Property(e => e.StartDate)
                    .IsRequired();

                sb.Property(e => e.EndDate)
                    .IsRequired(true);

                sb.Property(e => e.CreatedAt)
                    .IsRequired();

                sb.Property(e => e.ModifiedAt)
                    .IsRequired(false);

                sb.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                sb.Property(e => e.ModifiedBy)
                    .IsRequired(false)
                    .HasMaxLength(50);
            });
    }

    private void ConfigureProjectMemberTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder
            .OwnsMany(e => e.Members, sb =>
            {
                sb.ToTable("ProjectMember");

                sb.WithOwner().HasForeignKey("ProjectId");

                sb.HasKey("ProjectId", "Value");

                sb.Property(e => e.Value)
                    .HasColumnName("Username")
                    .ValueGeneratedNever()
                    .HasConversion(
                        username => username,
                        value => UserId.Create(value).Value
                    )
                    .HasMaxLength(50);
            });
    }

    private void ConfigureProjectHierarchyTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder
            .OwnsMany(e => e.Hierarchies, sb => 
            {
                sb.ToTable("ProjectHierarchy");

                sb.WithOwner().HasForeignKey("ProjectId");

                sb.HasKey("Id");
                sb.HasIndex("ProjectId");
                
                sb.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => HierarchyId.Create(value));

                sb.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                sb.Property(e => e.SuperiorHierarchyId)
                    .IsRequired(false)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id!.Value,
                        value => HierarchyId.Create(value));

                sb.OwnsMany(e => e.MemberUsernames, mb => {
                    mb.ToTable("ProjectHierarchyMember");

                    mb.WithOwner().HasForeignKey("HierarchyId");

                    mb.HasKey("HierarchyId", "Value");

                    mb.Property(e => e.Value)
                        .HasColumnName("Username")
                        .ValueGeneratedNever()
                        .HasConversion(
                            username => username,
                            value => UserId.Create(value).Value
                        )
                        .HasMaxLength(50);
                });

                sb.Property(e => e.CreatedAt)
                    .IsRequired();

                sb.Property(e => e.ModifiedAt)
                    .IsRequired(false);

                sb.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                sb.Property(e => e.ModifiedBy)
                    .IsRequired(false)
                    .HasMaxLength(50);
            });
    }
}
