using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voucher_category_IdCategory",
                table: "voucher");

            migrationBuilder.DropIndex(
                name: "IX_voucher_IdCategory",
                table: "voucher");

            migrationBuilder.DropColumn(
                name: "IdCategory",
                table: "voucher");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdCategory",
                table: "voucher",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_IdCategory",
                table: "voucher",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_voucher_category_IdCategory",
                table: "voucher",
                column: "IdCategory",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
