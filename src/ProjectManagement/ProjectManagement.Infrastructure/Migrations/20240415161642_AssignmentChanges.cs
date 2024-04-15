using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssignmentChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSubdomain");

            migrationBuilder.AddColumn<Guid>(
                name: "PhaseId",
                table: "ProjectAssignment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubdomainId",
                table: "ProjectAssignment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhaseId",
                table: "ProjectAssignment");

            migrationBuilder.DropColumn(
                name: "SubdomainId",
                table: "ProjectAssignment");

            migrationBuilder.CreateTable(
                name: "AssignmentSubdomain",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubdomainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubdomain", x => new { x.AssignmentId, x.SubdomainId });
                    table.ForeignKey(
                        name: "FK_AssignmentSubdomain_ProjectAssignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "ProjectAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
