using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIdLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectSubdomain_ProjectId",
                table: "ProjectSubdomain");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPhase_ProjectId",
                table: "ProjectPhase");

            migrationBuilder.DropIndex(
                name: "IX_ProjectAssignment_ProjectId",
                table: "ProjectAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAssignment_Project_ProjectId",
                table: "ProjectAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_Project_ProjectId",
                table: "ProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhase_Project_ProjectId",
                table: "ProjectPhase");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSubdomain_Project_ProjectId",
                table: "ProjectSubdomain");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMember",
                table: "ProjectMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubdomain",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectPhase",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectMember",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectAssignment",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Project",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMember",
                table: "ProjectMember",
                columns: new[] { "Username", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignment_Project_ProjectId",
                table: "ProjectAssignment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Project_ProjectId",
                table: "ProjectMember",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhase_Project_ProjectId",
                table: "ProjectPhase",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubdomain_Project_ProjectId",
                table: "ProjectSubdomain",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubdomain_ProjectId",
                table: "ProjectSubdomain",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhase_ProjectId",
                table: "ProjectPhase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_ProjectId",
                table: "ProjectMember",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignment_ProjectId",
                table: "ProjectAssignment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Id",
                table: "Project",
                column: "Id");
        }

        /// <inheritdoc />
        // adjust the Down method to revert the changes of Up method
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_ProjectMember_ProjectId", table: "ProjectMember");
            migrationBuilder.DropIndex(name: "IX_Project_Id", table: "Project");

            migrationBuilder.DropForeignKey(name: "FK_ProjectAssignment_Project_ProjectId", table: "ProjectAssignment");
            migrationBuilder.DropForeignKey(name: "FK_ProjectMember_Project_ProjectId", table: "ProjectMember");
            migrationBuilder.DropForeignKey(name: "FK_ProjectPhase_Project_ProjectId", table: "ProjectPhase");
            migrationBuilder.DropForeignKey(name: "FK_ProjectSubdomain_Project_ProjectId", table: "ProjectSubdomain");

            migrationBuilder.DropPrimaryKey(name: "PK_Project", table: "Project");
            migrationBuilder.DropPrimaryKey(name: "PK_ProjectMember", table: "ProjectMember");

            // Revert the column alterations
            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectSubdomain",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectPhase",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectMember",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "ProjectAssignment",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Project",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            // Recreate the original primary keys
            migrationBuilder.AddPrimaryKey(name: "PK_Project", table: "Project", column: "Id");
            migrationBuilder.AddPrimaryKey(name: "PK_ProjectMember", table: "ProjectMember", columns: new[] { "Username", "ProjectId" });

            // Recreate the original foreign keys
            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAssignment_Project_ProjectId",
                table: "ProjectAssignment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_Project_ProjectId",
                table: "ProjectMember",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhase_Project_ProjectId",
                table: "ProjectPhase",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSubdomain_Project_ProjectId",
                table: "ProjectSubdomain",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
