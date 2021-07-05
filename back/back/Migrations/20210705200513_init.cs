using Microsoft.EntityFrameworkCore.Migrations;

namespace back.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departement",
                columns: table => new
                {
                    idDep = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomDep = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departement", x => x.idDep);
                });

            migrationBuilder.CreateTable(
                name: "pain",
                columns: table => new
                {
                    idPain = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomPain = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pain", x => x.idPain);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departement");

            migrationBuilder.DropTable(
                name: "pain");
        }
    }
}
