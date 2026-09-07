using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProduitsApi.Migrations
{
    /// <inheritdoc />
    public partial class correctCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreation",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreation",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreation",
                value: new DateTime(2026, 9, 7, 13, 30, 7, 346, DateTimeKind.Utc).AddTicks(9542));

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreation",
                value: new DateTime(2026, 9, 7, 13, 30, 7, 347, DateTimeKind.Utc).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Produits",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreation",
                value: new DateTime(2026, 9, 7, 13, 30, 7, 347, DateTimeKind.Utc).AddTicks(465));
        }
    }
}
