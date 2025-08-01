using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiMenu_Back.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                })
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
