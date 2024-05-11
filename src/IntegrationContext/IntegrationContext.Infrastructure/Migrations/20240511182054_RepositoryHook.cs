using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegrationContext.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RepositoryHook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiteaRepositoryHook",
                schema: "integration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    HookUri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Events = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GiteaRepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiteaRepositoryHook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiteaRepositoryHook_GiteaRepository_GiteaRepositoryId",
                        column: x => x.GiteaRepositoryId,
                        principalSchema: "integration",
                        principalTable: "GiteaRepository",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiteaRepositoryHook_GiteaRepositoryId",
                schema: "integration",
                table: "GiteaRepositoryHook",
                column: "GiteaRepositoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiteaRepositoryHook",
                schema: "integration");
        }
    }
}
