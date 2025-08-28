using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class AddTableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "payment",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdUser = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IdPayment = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RetirementTime = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    RetirementInstruction = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPublic = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_payment_IdPayment",
                        column: x => x.IdPayment,
                        principalTable: "payment",
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
                name: "IX_order_IdPayment",
                table: "order",
                column: "IdPayment",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_IdUser",
                table: "order",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "payment",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
