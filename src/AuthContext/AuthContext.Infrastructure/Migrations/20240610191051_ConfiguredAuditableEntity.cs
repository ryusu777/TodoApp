﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "auth",
                table: "UserRefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "auth",
                table: "UserRefreshTokens",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                schema: "auth",
                table: "UserRefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "auth",
                table: "UserRefreshTokens",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "auth",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "auth",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                schema: "auth",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "auth",
                table: "UserRefreshTokens");
        }
    }
}
