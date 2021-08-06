using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CuentaVirtual.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapitalPesos = table.Column<float>(type: "real", nullable: false),
                    CapitalDolares = table.Column<float>(type: "real", nullable: false),
                    CapitalCriptomonedas = table.Column<float>(type: "real", nullable: false),
                    ImgDoc1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImgDoc2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
