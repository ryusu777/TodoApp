using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Infrastructure.Subdomain;

public class SubdomainEntityConfiguration : IEntityTypeConfiguration<Domain.Subdomain.Subdomain>
{
	public void Configure(EntityTypeBuilder<Domain.Subdomain.Subdomain> builder)
	{
		ConfigureSubdomainTable(builder);
		ConfigureSubdomainKnowledgeTable(builder);
	}

	public void ConfigureSubdomainTable(EntityTypeBuilder<Domain.Subdomain.Subdomain> builder)
	{
		builder.ToTable("ProjectSubdomain");

		builder.HasKey(e => e.Id);

		builder.Property(e => e.Id)
			.ValueGeneratedNever()
			.HasConversion(
				id => id.Value,
				value => SubdomainId.Create(value)
			);

		builder.Property(e => e.Description)
			.IsRequired()
			.HasMaxLength(200);

		builder.Property(e => e.Title)
			.IsRequired()
			.HasMaxLength(30);

		builder.Property(e => e.ProjectId)
            .HasConversion(
                id => id.Value,
                value => ProjectId.Create(value));

		builder
			.HasOne<Domain.Project.Project>()
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
            .IsRequired()
            .HasMaxLength(50);
	}

	public void ConfigureSubdomainKnowledgeTable(EntityTypeBuilder<Domain.Subdomain.Subdomain> builder)
	{

		builder.OwnsMany(e => e.Knowledges, kb =>
		{
			kb.ToTable("SubdomainKnowledge");

			kb.WithOwner().HasForeignKey(e => e.SubdomainId);

			kb.HasKey(e => e.Id);

			kb.Property(e => e.Id)
				.ValueGeneratedNever()
				.HasConversion(
					id => id.Value,
					value => SubdomainKnowledgeId.Create(value));

			kb.Property(e => e.SubdomainId)
				.ValueGeneratedNever()
				.HasConversion(
					id => id.Value,
					value => SubdomainId.Create(value));

			kb.Property(e => e.Title)
				.IsRequired()
				.HasMaxLength(30);

			kb.Property(e => e.Content)
				.IsRequired()
				.HasColumnType("text");

            kb.Property(e => e.CreatedAt)
                .IsRequired();

            kb.Property(e => e.ModifiedAt)
                .IsRequired(false);

            kb.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            kb.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);
            });
	}
}
