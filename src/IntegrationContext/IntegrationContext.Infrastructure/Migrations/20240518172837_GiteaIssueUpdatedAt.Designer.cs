﻿// <auto-generated />
using System;
using IntegrationContext.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240518172837_GiteaIssueUpdatedAt")]
    partial class GiteaIssueUpdatedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("integration")
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntegrationContext.Domain.Auth.GiteaUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("JwtToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GiteaUser", "integration");
                });

            modelBuilder.Entity("IntegrationContext.Domain.CommandOutboxes.CommandOutbox", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommandDetail")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CommandResult")
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(sysdatetime())");

                    b.Property<string>("LastError")
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("LastExecutionAt")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(sysdatetime())");

                    b.Property<int>("MaxTries")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SuccessAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Tries")
                        .HasColumnType("int")
                        .HasColumnName("Retries");

                    b.HasKey("Id");

                    b.ToTable("CommandOutbox", "integration");
                });

            modelBuilder.Entity("IntegrationContext.Domain.GiteaIssues.GiteaIssue", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiteaRepositoryId")
                        .HasColumnType("int");

                    b.Property<int>("IssueNumber")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("GiteaRepositoryId");

                    b.ToTable("GiteaIssue", "integration");
                });

            modelBuilder.Entity("IntegrationContext.Domain.GiteaRepositories.GiteaRepository", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RepoName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("RepoOwner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GiteaRepository", "integration");
                });

            modelBuilder.Entity("IntegrationContext.Domain.GiteaIssues.GiteaIssue", b =>
                {
                    b.HasOne("IntegrationContext.Domain.GiteaRepositories.GiteaRepository", null)
                        .WithMany()
                        .HasForeignKey("GiteaRepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntegrationContext.Domain.GiteaRepositories.GiteaRepository", b =>
                {
                    b.OwnsMany("IntegrationContext.Domain.GiteaRepositories.Entities.RepositoryHook", "Hooks", b1 =>
                        {
                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<bool>("Active")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bit")
                                .HasDefaultValue(true);

                            b1.Property<string>("Events")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar");

                            b1.Property<int>("GiteaRepositoryId")
                                .HasColumnType("int");

                            b1.Property<string>("HookUri")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("Id");

                            b1.HasIndex("GiteaRepositoryId");

                            b1.ToTable("GiteaRepositoryHook", "integration");

                            b1.WithOwner()
                                .HasForeignKey("GiteaRepositoryId");
                        });

                    b.Navigation("Hooks");
                });
#pragma warning restore 612, 618
        }
    }
}