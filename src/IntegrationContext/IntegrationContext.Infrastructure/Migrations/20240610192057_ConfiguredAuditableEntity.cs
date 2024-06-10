using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaRepositoryHook",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaRepositoryHook",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaRepositoryHook",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaRepositoryHook",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaRepository",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaRepository",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaRepository",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaRepository",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaIssue",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaIssue",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaIssue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaIssue",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "integration",
                table: "CommandOutbox",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "integration",
                table: "CommandOutbox",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "integration",
                table: "CommandOutbox",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaUser");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaRepositoryHook");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaRepositoryHook");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaRepositoryHook");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaRepositoryHook");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaRepository");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaRepository");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaRepository");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaRepository");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "integration",
                table: "GiteaIssue");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "integration",
                table: "GiteaIssue");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "integration",
                table: "GiteaIssue");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "integration",
                table: "GiteaIssue");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "integration",
                table: "CommandOutbox");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "integration",
                table: "CommandOutbox");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "integration",
                table: "CommandOutbox");
        }
    }
}
