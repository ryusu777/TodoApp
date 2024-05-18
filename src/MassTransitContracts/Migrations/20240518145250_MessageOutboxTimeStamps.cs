﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTransitContracts.Migrations
{
    /// <inheritdoc />
    public partial class MessageOutboxTimeStamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "messaging",
                table: "OutboxMessage",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(sysdatetime())");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTryAt",
                schema: "messaging",
                table: "OutboxMessage",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(sysdatetime())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "messaging",
                table: "OutboxMessage");

            migrationBuilder.DropColumn(
                name: "LastTryAt",
                schema: "messaging",
                table: "OutboxMessage");
        }
    }
}
