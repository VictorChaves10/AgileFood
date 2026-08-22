using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Consumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsumptionId",
                table: "StockMovements",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Origin",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consumptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceMonth = table.Column<int>(type: "int", nullable: false),
                    ReferenceYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumptionId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionItems_Consumptions_ConsumptionId",
                        column: x => x.ConsumptionId,
                        principalTable: "Consumptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ConsumptionId",
                table: "StockMovements",
                column: "ConsumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_ProductId",
                table: "CatalogItems",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionItems_ConsumptionId",
                table: "ConsumptionItems",
                column: "ConsumptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_User_Period",
                table: "Consumptions",
                columns: new[] { "UserId", "ReferenceYear", "ReferenceMonth" });

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_Consumptions_ConsumptionId",
                table: "StockMovements",
                column: "ConsumptionId",
                principalTable: "Consumptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_Consumptions_ConsumptionId",
                table: "StockMovements");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "ConsumptionItems");

            migrationBuilder.DropTable(
                name: "Consumptions");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ConsumptionId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "ConsumptionId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "StockMovements");
        }
    }
}
