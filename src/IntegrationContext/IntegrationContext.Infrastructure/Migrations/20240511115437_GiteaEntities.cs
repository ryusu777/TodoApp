using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GiteaEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiteaRepository",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RepoOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepoName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaRepository", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiteaIssue",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiteaRepositoryId = table.Column<int>(type: "int", nullable: false),
                    IssueNumber = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_GiteaIssue_GiteaRepositoryId",
                schema: "integration",
                table: "GiteaIssue",
                column: "GiteaRepositoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiteaIssue",
                schema: "integration");

            migrationBuilder.DropTable(
                name: "GiteaRepository",
                schema: "integration");
        }
    }
}
