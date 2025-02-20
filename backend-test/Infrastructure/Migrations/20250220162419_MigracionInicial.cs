using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcaAutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    PaisOrigen = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaAutos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MarcaAutos",
                columns: new[] { "Id", "Nombre", "PaisOrigen" },
                values: new object[,]
                {
                    { 1, "Toyota", "Japon" },
                    { 2, "Ford", "Estados Unidos" },
                    { 3, "Chevrolet", "Estados Unidos" },
                    { 4, "Nissan", "Japon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcaAutos");
        }
    }
}
