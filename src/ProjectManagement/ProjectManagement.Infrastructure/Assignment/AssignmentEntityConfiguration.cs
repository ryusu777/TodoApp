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
		ConfigureAssignmentSubdomainTable(builder);
		ConfigureAssignmentAsigneeTable(builder);
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

		builder.Property(e => e.ProjectId)
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => ProjectId.Create(value)
			);

		builder.HasOne<Domain.Project.Project>()
			.WithMany()
			.HasForeignKey(e => e.ProjectId)
			.IsRequired();
	}

	private void ConfigureAssignmentSubdomainTable(EntityTypeBuilder<Domain.Assignment.Assignment> builder)
	{
		builder.OwnsMany(e => e.SubdomainIds, sb =>
		{
			sb.ToTable("AssignmentSubdomain");

			sb.WithOwner().HasForeignKey("AssignmentId");

			sb.Property(e => e.Value)
				.HasColumnName("SubdomainId")
				.ValueGeneratedNever();

			sb.HasKey("AssignmentId", "Value");
		});
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
}
