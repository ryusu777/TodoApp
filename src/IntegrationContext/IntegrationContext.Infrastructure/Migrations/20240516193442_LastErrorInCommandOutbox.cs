using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LastErrorInCommandOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastError",
                schema: "integration",
                table: "CommandOutbox",
                type: "nvarchar(2000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastError",
                schema: "integration",
                table: "CommandOutbox");
        }
    }
}
