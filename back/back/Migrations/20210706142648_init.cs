using System;
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
                    nomPain = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pain", x => x.idPain);
                });

            migrationBuilder.CreateTable(
                name: "utilisateur",
                columns: table => new
                {
                    idUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomUtilisateur = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    prenomUtilisateur = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    adresseUtilisateur = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    mailUtilisateur = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    mdpUtilisateur = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilisateur", x => x.idUtilisateur);
                });

            migrationBuilder.CreateTable(
                name: "commande",
                columns: table => new
                {
                    idCommande = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUtilisateur = table.Column<int>(type: "int", nullable: false),
                    dateLivraisonCommande = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande", x => x.idCommande);
                    table.ForeignKey(
                        name: "FK_commande_utilisateur_idUtilisateur",
                        column: x => x.idUtilisateur,
                        principalTable: "utilisateur",
                        principalColumn: "idUtilisateur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "painCommande",
                columns: table => new
                {
                    idCommande = table.Column<int>(type: "int", nullable: false),
                    idPain = table.Column<int>(type: "int", nullable: false),
                    qte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_painCommande", x => new { x.idCommande, x.idPain });
                    table.ForeignKey(
                        name: "FK_painCommande_commande_idCommande",
                        column: x => x.idCommande,
                        principalTable: "commande",
                        principalColumn: "idCommande",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_painCommande_pain_idPain",
                        column: x => x.idPain,
                        principalTable: "pain",
                        principalColumn: "idPain",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_commande_idUtilisateur",
                table: "commande",
                column: "idUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_painCommande_idPain",
                table: "painCommande",
                column: "idPain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departement");

            migrationBuilder.DropTable(
                name: "painCommande");

            migrationBuilder.DropTable(
                name: "commande");

            migrationBuilder.DropTable(
                name: "pain");

            migrationBuilder.DropTable(
                name: "utilisateur");
        }
    }
}
