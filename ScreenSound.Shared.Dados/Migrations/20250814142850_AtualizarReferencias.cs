﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarReferencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.Sql("UPDATE musicas SET ArtistaId = 1 WHERE Id = 1");
            migrationBuilder.Sql("UPDATE musicas SET ArtistaId = 1 WHERE Id = 2");
            migrationBuilder.Sql("UPDATE musicas SET ArtistaId = 1 WHERE Id = 3");
            migrationBuilder.Sql("UPDATE musicas SET ArtistaId = 1 WHERE Id = 4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
