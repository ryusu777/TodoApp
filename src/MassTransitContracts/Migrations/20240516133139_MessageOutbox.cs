using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassTransitContracts.Migrations
{
    /// <inheritdoc />
    public partial class MessageOutbox : Migration
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDetail = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Tries = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MaxTries = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
