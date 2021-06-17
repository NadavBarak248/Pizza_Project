using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class pi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {








            migrationBuilder.CreateTable(
                name: "PizzaImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image_content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaImage_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });




            migrationBuilder.CreateIndex(
                name: "IX_PizzaImage_PizzaId",
                table: "PizzaImage",
                column: "PizzaId",
                unique: true);



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "PizzaImage");


        }
    }
}
