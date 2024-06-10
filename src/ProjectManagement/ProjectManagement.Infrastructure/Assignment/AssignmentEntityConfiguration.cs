using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Assignment.ValueObjects;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Infrastructure.Assignment;

public class AssignmentEntityConfiguration : IEntityTypeConfiguration<Domain.Assignment.Assignment>
{
	public void Configure(EntityTypeBuilder<Domain.Assignment.Assignment> builder)
	{
		ConfigureAssignmentTable(builder);
		ConfigureAssignmentAsigneeTable(builder);
        ConfigureAssignmentReviewTable(builder);
	}

	private void ConfigureAssignmentTable(EntityTypeBuilder<Domain.Assignment.Assignment> builder)
	{
		builder.ToTable("ProjectAssignment");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id)
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => AssignmentId.Create(value)
			);

		builder.Property(e => e.Status)
			.HasConversion(
				status => status.Value,
				value => AssignmentStatus.Create(value.ToString()).Value!
			)
			.IsRequired()
			.HasColumnType("varchar(20)");

		builder.Property(e => e.Title)
			.IsRequired()
			.HasMaxLength(50);

		builder.Property(e => e.Description)
			.IsRequired(false)
			.HasMaxLength(50);

        builder.Property(e => e.Deadline)
            .IsRequired(false);

		builder.Property(e => e.ProjectId)
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => ProjectId.Create(value)
			);

		builder.Property(e => e.SubdomainId)
			.ValueGeneratedNever()
            .IsRequired(false)
			.HasConversion(
				id => id!.Value,
				value => SubdomainId.Create(value)
			);

		builder.Property(e => e.PhaseId)
			.ValueGeneratedNever()
            .IsRequired(false)
			.HasConversion(
				id => id!.Value,
				value => PhaseId.Create(value)
			);

		builder.Property(e => e.Reviewer)
			.ValueGeneratedNever()
            .IsRequired(false)
			.HasConversion(
				id => id!.Value,
				value => UserId.Create(value))
            .HasMaxLength(50);

		builder.HasOne<Domain.Project.Project>()
			.WithMany()
			.HasForeignKey(e => e.ProjectId)
			.IsRequired();

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

	private void ConfigureAssignmentAsigneeTable(EntityTypeBuilder<Domain.Assignment.Assignment> builder)
	{
		builder.OwnsMany(e => e.Assignees, sb =>
		{
			sb.ToTable("AssignmentAssignee");

			sb.WithOwner().HasForeignKey("AssignmentId");

			sb.Property(e => e.Value)
				.HasColumnName("Username")
				.ValueGeneratedNever();

			sb.HasKey("AssignmentId", "Value");
		});
	}

    private void ConfigureAssignmentReviewTable(EntityTypeBuilder<Domain.Assignment.Assignment> builder)
    {
        builder.OwnsMany(e => e.Reviews, sb =>
        {
            sb.ToTable("AssignmentReview");

            sb.WithOwner().HasForeignKey("AssignmentId");

            sb.HasKey(e => e.Id);

            sb.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReviewId.Create(value)
                );

            sb.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);

            sb.Property(e => e.RejectionNotes)
                .IsRequired(false)
                .HasMaxLength(200);

            sb.Property(e => e.Status)
                .IsRequired()
                .HasColumnType("varchar(20)");

            sb.Property(e => e.Reviewer)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

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
