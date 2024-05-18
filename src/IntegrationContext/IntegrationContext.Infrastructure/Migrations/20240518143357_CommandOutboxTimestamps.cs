using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CommandOutboxTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tries",
                schema: "integration",
                table: "CommandOutbox",
                newName: "Retries");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "integration",
                table: "CommandOutbox",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(sysdatetime())");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastExecutionAt",
                schema: "integration",
                table: "CommandOutbox",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(sysdatetime())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "integration",
                table: "CommandOutbox");

            migrationBuilder.DropColumn(
                name: "LastExecutionAt",
                schema: "integration",
                table: "CommandOutbox");

            migrationBuilder.RenameColumn(
                name: "Retries",
                schema: "integration",
                table: "CommandOutbox",
                newName: "Tries");
        }
    }
}
