﻿using Microsoft.EntityFrameworkCore;
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
        ConfigureProjectMembersTable(builder);
    }

    private void ConfigureProjectTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder.ToTable("Project");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
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
            });
    }

    private void ConfigureProjectMembersTable(EntityTypeBuilder<Domain.Project.Project> builder)
    {
        builder.OwnsMany(e => e.ProjectMembers, sb =>
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
                );
        });
    }
}