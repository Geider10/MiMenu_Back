using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRol",
                table: "user",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldDefaultValue: new Guid("00000000-0000-0000-0000-000000000001"))
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "cartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUser = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdFood = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceTotal = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cartItem_food_IdFood",
                        column: x => x.IdFood,
                        principalTable: "food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartItem_user_IdUser",
                        column: x => x.IdUser,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cartItem_IdFood",
                table: "cartItem",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_cartItem_IdUser",
                table: "cartItem",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRol",
                table: "user",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000001"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdFood = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUser = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PriceTotal = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_food_IdFood",
                        column: x => x.IdFood,
                        principalTable: "food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_user_IdUser",
                        column: x => x.IdUser,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_order_IdFood",
                table: "order",
                column: "IdFood");

            migrationBuilder.CreateIndex(
                name: "IX_order_IdUser",
                table: "order",
                column: "IdUser");
        }
    }
}
