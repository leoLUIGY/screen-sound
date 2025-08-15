using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarArtistaMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistaId",
                table: "musicas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_musicas_ArtistaId",
                table: "musicas",
                column: "ArtistaId");

            migrationBuilder.AddForeignKey(
                name: "FK_musicas_Artistas_ArtistaId",
                table: "musicas",
                column: "ArtistaId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_musicas_Artistas_ArtistaId",
                table: "musicas");

            migrationBuilder.DropIndex(
                name: "IX_musicas_ArtistaId",
                table: "musicas");

            migrationBuilder.DropColumn(
                name: "ArtistaId",
                table: "musicas");
        }
    }
}
