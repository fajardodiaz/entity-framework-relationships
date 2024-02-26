using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class CharacterFaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFaction");

            migrationBuilder.CreateTable(
                name: "CharacterFactions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    FactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFactions", x => new { x.CharacterId, x.FactionId });
                    table.ForeignKey(
                        name: "FK_CharacterFactions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFactions_Factions_FactionId",
                        column: x => x.FactionId,
                        principalTable: "Factions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFactions_FactionId",
                table: "CharacterFactions",
                column: "FactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterFactions");

            migrationBuilder.CreateTable(
                name: "CharacterFaction",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    FactionsId = table.Column<int>(type: "int", nullable: false)
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
    }
}
