using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.MigrationsPgSql
{
    /// <inheritdoc />
    public partial class InitPgSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "integration");

            migrationBuilder.CreateTable(
                name: "CommandOutbox",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommandDetail = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CommandResult = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    LastError = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Retries = table.Column<int>(type: "integer", nullable: false),
                    MaxTries = table.Column<int>(type: "integer", nullable: false),
                    SuccessAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastExecutionAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2025, 1, 10, 12, 4, 1, 312, DateTimeKind.Local).AddTicks(4558)),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandOutbox", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiteaRepository",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RepoOwner = table.Column<string>(type: "text", nullable: false),
                    RepoName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaRepository", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiteaUser",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    JwtToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiteaIssue",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    GiteaRepositoryId = table.Column<int>(type: "integer", nullable: false),
                    IssueNumber = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiteaIssue_GiteaRepository_GiteaRepositoryId",
                        column: x => x.GiteaRepositoryId,
                        principalSchema: "integration",
                        principalTable: "GiteaRepository",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiteaRepositoryHook",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    HookUri = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Events = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    GiteaRepositoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaRepositoryHook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiteaRepositoryHook_GiteaRepository_GiteaRepositoryId",
                        column: x => x.GiteaRepositoryId,
                        principalSchema: "integration",
                        principalTable: "GiteaRepository",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiteaIssue_GiteaRepositoryId",
                schema: "integration",
                table: "GiteaIssue",
                column: "GiteaRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GiteaRepositoryHook_GiteaRepositoryId",
                schema: "integration",
                table: "GiteaRepositoryHook",
                column: "GiteaRepositoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandOutbox",
                schema: "integration");

            migrationBuilder.DropTable(
                name: "GiteaIssue",
                schema: "integration");

            migrationBuilder.DropTable(
                name: "GiteaRepositoryHook",
                schema: "integration");

            migrationBuilder.DropTable(
                name: "GiteaUser",
                schema: "integration");

            migrationBuilder.DropTable(
                name: "GiteaRepository",
                schema: "integration");
        }
    }
}
