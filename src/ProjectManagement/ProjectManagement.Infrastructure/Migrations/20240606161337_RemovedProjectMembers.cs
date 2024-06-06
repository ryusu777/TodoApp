using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProjectMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMember");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectMember",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => new { x.ProjectId, x.Username });
                    table.ForeignKey(
                        name: "FK_ProjectMember_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
