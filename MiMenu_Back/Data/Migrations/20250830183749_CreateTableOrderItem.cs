using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdOrder = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdFood = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orderItem_food_IdFood",
                        column: x => x.IdFood,
                        principalTable: "food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderItem_order_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_IdFood",
                table: "orderItem",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_IdOrder",
                table: "orderItem",
                column: "IdOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItem");
        }
    }
}
