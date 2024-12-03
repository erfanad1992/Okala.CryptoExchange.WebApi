using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Okala.CryptoExchange.EfPersistance.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Cryptocurrency",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrencyQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CryptocurrencyId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencyQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoCurrencyQuotes_Cryptocurrency_CryptocurrencyId",
                        column: x => x.CryptocurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Cryptocurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CryptoCurrencyQuotes_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "dbo",
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoCurrencyQuotes_CryptocurrencyId",
                table: "CryptoCurrencyQuotes",
                column: "CryptocurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoCurrencyQuotes_CurrencyId",
                table: "CryptoCurrencyQuotes",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Id",
                schema: "dbo",
                table: "Currency",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrencyQuotes");

            migrationBuilder.DropTable(
                name: "Cryptocurrency",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Currency",
                schema: "dbo");
        }
    }
}
