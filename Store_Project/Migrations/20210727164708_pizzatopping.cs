using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class pizzatopping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Pizza_PizzaId",
                table: "Pizza");

            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Topping_ToppingId",
                table: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_PizzaId",
                table: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_ToppingId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "ToppingId",
                table: "Pizza");

            migrationBuilder.CreateTable(
                name: "PizzaTopping",
                columns: table => new
                {
                    Pizza_toppingsId = table.Column<int>(type: "int", nullable: false),
                    Toppings_pizzaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTopping", x => new { x.Pizza_toppingsId, x.Toppings_pizzaId });
                    table.ForeignKey(
                        name: "FK_PizzaTopping_Pizza_Toppings_pizzaId",
                        column: x => x.Toppings_pizzaId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTopping_Topping_Pizza_toppingsId",
                        column: x => x.Pizza_toppingsId,
                        principalTable: "Topping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTopping_Toppings_pizzaId",
                table: "PizzaTopping",
                column: "Toppings_pizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaTopping");

            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToppingId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_PizzaId",
                table: "Pizza",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_ToppingId",
                table: "Pizza",
                column: "ToppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Pizza_PizzaId",
                table: "Pizza",
                column: "PizzaId",
                principalTable: "Pizza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Topping_ToppingId",
                table: "Pizza",
                column: "ToppingId",
                principalTable: "Topping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
