using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProduitsApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Prix = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Produits",
                columns: new[] { "Id", "DateCreation", "Description", "Nom", "Prix", "Stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 7, 13, 30, 7, 346, DateTimeKind.Utc).AddTicks(9542), "Clavier mécanique", "Clavier", 79.90m, 12 },
                    { 2, new DateTime(2026, 9, 7, 13, 30, 7, 347, DateTimeKind.Utc).AddTicks(462), "Souris sans fil", "Souris", 29.90m, 30 },
                    { 3, new DateTime(2026, 9, 7, 13, 30, 7, 347, DateTimeKind.Utc).AddTicks(465), "Écran 27 pouces", "Écran", 199.00m, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produits");
        }
    }
}
