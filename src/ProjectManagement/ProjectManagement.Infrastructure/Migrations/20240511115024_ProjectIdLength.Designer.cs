﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagement.Domain.Assignment.Enums;
using ProjectManagement.Infrastructure.Persistence.Data;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240511115024_ProjectIdLength")]
    partial class ProjectIdLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectManagement.Domain.Assignment.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("PhaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Reviewer")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<AssignmentStatusEnum>("Status")
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("SubdomainId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectAssignment", (string)null);
                });

            modelBuilder.Entity("ProjectManagement.Domain.Project.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ProjectManagement.Domain.Subdomain.Subdomain", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectSubdomain", (string)null);
                });

            modelBuilder.Entity("ProjectManagement.Domain.Assignment.Assignment", b =>
                {
                    b.HasOne("ProjectManagement.Domain.Project.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ProjectManagement.Domain.Common.ValueObjects.UserId", "Assignees", b1 =>
                        {
                            b1.Property<Guid>("AssignmentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("Username");

                            b1.HasKey("AssignmentId", "Value");

                            b1.ToTable("AssignmentAssignee", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("AssignmentId");
                        });

                    b.Navigation("Assignees");
                });

            modelBuilder.Entity("ProjectManagement.Domain.Project.Project", b =>
                {
                    b.OwnsMany("ProjectManagement.Domain.Project.Entities.Phase", "ProjectPhases", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<DateOnly>("EndDate")
                                .HasColumnType("date");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("ProjectId")
                                .IsRequired()
                                .HasColumnType("nvarchar(30)");

                            b1.Property<DateOnly>("StartDate")
                                .HasColumnType("date");

                            b1.HasKey("Id");

                            b1.HasIndex("ProjectId");

                            b1.ToTable("ProjectPhase", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsMany("ProjectManagement.Domain.Common.ValueObjects.UserId", "ProjectMembers", b1 =>
                        {
                            b1.Property<string>("ProjectId")
                                .HasColumnType("nvarchar(30)");

                            b1.Property<string>("Value")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Username");

                            b1.HasKey("ProjectId", "Value");

                            b1.ToTable("ProjectMember", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("ProjectMembers");

                    b.Navigation("ProjectPhases");
                });

            modelBuilder.Entity("ProjectManagement.Domain.Subdomain.Subdomain", b =>
                {
                    b.HasOne("ProjectManagement.Domain.Project.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ProjectManagement.Domain.Subdomain.Entities.SubdomainKnowledge", "Knowledges", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Content")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<Guid>("SubdomainId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(30)
                                .HasColumnType("nvarchar(30)");

                            b1.HasKey("Id");

                            b1.HasIndex("SubdomainId");

                            b1.ToTable("SubdomainKnowledge", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SubdomainId");
                        });

                    b.Navigation("Knowledges");
                });
#pragma warning restore 612, 618
        }
    }
}