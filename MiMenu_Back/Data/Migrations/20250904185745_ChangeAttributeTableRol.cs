using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttributeTableRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_food_IdFood",
                table: "orderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rol_IdRol",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_IdRol",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "rol");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "rol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Type",
                value: 0);

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Type",
                value: 1);

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Type",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_user_IdRol",
                table: "user",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_food_IdFood",
                table: "orderItem",
                column: "IdFood",
                principalTable: "food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_rol_IdRol",
                table: "user",
                column: "IdRol",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItem_food_IdFood",
                table: "orderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rol_IdRol",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_IdRol",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "rol");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "rol",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Name",
                value: "client");

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "Name",
                value: "local");

            migrationBuilder.CreateIndex(
                name: "IX_user_IdRol",
                table: "user",
                column: "IdRol",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItem_food_IdFood",
                table: "orderItem",
                column: "IdFood",
                principalTable: "food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_rol_IdRol",
                table: "user",
                column: "IdRol",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
