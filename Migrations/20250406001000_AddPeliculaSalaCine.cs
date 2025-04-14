using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPeliculaSalaCine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaSalaCine");

            migrationBuilder.CreateTable(
                name: "PeliculasSalasCine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPelicula = table.Column<int>(type: "integer", nullable: false),
                    IdSalaCine = table.Column<int>(type: "integer", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSalasCine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeliculasSalasCine_Peliculas_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Peliculas",
                        principalColumn: "IdPelicula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasSalasCine_SalasCine_IdSalaCine",
                        column: x => x.IdSalaCine,
                        principalTable: "SalasCine",
                        principalColumn: "IdSala",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasCine_IdPelicula",
                table: "PeliculasSalasCine",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasCine_IdSalaCine",
                table: "PeliculasSalasCine",
                column: "IdSalaCine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasSalasCine");

            migrationBuilder.CreateTable(
                name: "PeliculaSalaCine",
                columns: table => new
                {
                    IdPeliculaSala = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPelicula = table.Column<int>(type: "integer", nullable: false),
                    IdSalaCine = table.Column<int>(type: "integer", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaSalaCine", x => x.IdPeliculaSala);
                    table.ForeignKey(
                        name: "FK_PeliculaSalaCine_Peliculas_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "Peliculas",
                        principalColumn: "IdPelicula",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaSalaCine_SalasCine_IdSalaCine",
                        column: x => x.IdSalaCine,
                        principalTable: "SalasCine",
                        principalColumn: "IdSala",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSalaCine_IdPelicula",
                table: "PeliculaSalaCine",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaSalaCine_IdSalaCine",
                table: "PeliculaSalaCine",
                column: "IdSalaCine");
        }
    }
}
