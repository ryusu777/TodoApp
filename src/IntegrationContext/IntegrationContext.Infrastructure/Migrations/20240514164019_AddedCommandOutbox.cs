using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommandOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JwtExpiresAt",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresAt",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.CreateTable(
                name: "CommandOutbox",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommandDetail = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    CommandResult = table.Column<string>(type: "nvarchar(2000)", nullable: true),
                    Tries = table.Column<int>(type: "int", nullable: false),
                    MaxTries = table.Column<int>(type: "int", nullable: false),
                    SuccessAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandOutbox", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandOutbox",
                schema: "integration");

            migrationBuilder.AddColumn<DateTime>(
                name: "JwtExpiresAt",
                schema: "integration",
                table: "GiteaUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresAt",
                schema: "integration",
                table: "GiteaUser",
                type: "datetime2",
                nullable: true);
        }
    }
}
