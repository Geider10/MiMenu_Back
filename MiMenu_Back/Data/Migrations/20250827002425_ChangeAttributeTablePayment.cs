using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributeTablePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_user_IdUser",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_payment_IdUser",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "PaymentTotal",
                table: "payment");

            migrationBuilder.RenameColumn(
                name: "IdPublicMP",
                table: "payment",
                newName: "IdPublic");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "payment",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "payment");

            migrationBuilder.RenameColumn(
                name: "IdPublic",
                table: "payment",
                newName: "IdPublicMP");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "payment",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<double>(
                name: "PaymentTotal",
                table: "payment",
                type: "double",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_IdUser",
                table: "payment",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_user_IdUser",
                table: "payment",
                column: "IdUser",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
