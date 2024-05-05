using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiteaUserId",
                schema: "auth",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GiteaUserId",
                schema: "auth",
                table: "User",
                type: "int",
                nullable: true);
        }
    }
}
