using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class DBV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_food_IdFood",
                table: "order");

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("0a1ac601-880b-4cca-b2b8-7e79ccfe3351"));

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("47686657-4e14-412c-8cc1-ea54d2c642b4"));

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("d30e38f1-f0f8-40d9-96ed-81b8d992a764"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "user",
                newName: "Phone");

            migrationBuilder.AddColumn<Guid>(
                name: "IdRol",
                table: "user",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000001"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "rol",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "client" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "admin" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "local" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_IdRol",
                table: "user",
                column: "IdRol",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_food_IdFood",
                table: "order",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_food_IdFood",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rol_IdRol",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_IdRol",
                table: "user");

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "rol",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "user",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "user",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "rol",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0a1ac601-880b-4cca-b2b8-7e79ccfe3351"), "client" },
                    { new Guid("47686657-4e14-412c-8cc1-ea54d2c642b4"), "admin" },
                    { new Guid("d30e38f1-f0f8-40d9-96ed-81b8d992a764"), "local" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_order_food_IdFood",
                table: "order",
                column: "IdFood",
                principalTable: "food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
