using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTransitContracts.Migrations
{
    /// <inheritdoc />
    public partial class InitPgSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "messaging");

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "messaging",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventDetail = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Tries = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    MaxTries = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastTryAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "messaging");
        }
    }
}
