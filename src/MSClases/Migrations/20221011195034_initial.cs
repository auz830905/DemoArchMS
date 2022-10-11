using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSClases.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaseProfesores",
                columns: table => new
                {
                    Ci = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IdClase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseProfesores", x => new { x.IdClase, x.Ci });
                    table.ForeignKey(
                        name: "FK_CLASE",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaseProfesores");

            migrationBuilder.DropTable(
                name: "Clases");
        }
    }
}
