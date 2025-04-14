using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPeliculaSalaCineRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasSalasCine",
                table: "PeliculasSalasCine");

            migrationBuilder.DropIndex(
                name: "IX_PeliculasSalasCine_IdPelicula",
                table: "PeliculasSalasCine");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PeliculasSalasCine",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasSalasCine",
                table: "PeliculasSalasCine",
                columns: new[] { "IdPelicula", "IdSalaCine" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasSalasCine",
                table: "PeliculasSalasCine");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PeliculasSalasCine",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasSalasCine",
                table: "PeliculasSalasCine",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasCine_IdPelicula",
                table: "PeliculasSalasCine",
                column: "IdPelicula");
        }
    }
}
