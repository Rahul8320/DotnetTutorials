using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreRelationShips.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class FactionCharacterRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFaction",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "INTEGER", nullable: false),
                    FactionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFaction", x => new { x.CharactersId, x.FactionsId });
                    table.ForeignKey(
                        name: "FK_CharacterFaction_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFaction_Factions_FactionsId",
                        column: x => x.FactionsId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFaction_FactionsId",
                table: "CharacterFaction",
                column: "FactionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFaction");

            migrationBuilder.DropTable(
                name: "Factions");
        }
    }
}
