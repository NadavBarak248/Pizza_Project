using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class pizzasinorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPizza");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPizza",
                columns: table => new
                {
                    Order_pizzaId = table.Column<int>(type: "int", nullable: false),
                    Pizza_orderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPizza", x => new { x.Order_pizzaId, x.Pizza_orderId });
                    table.ForeignKey(
                        name: "FK_OrderPizza_Order_Order_pizzaId",
                        column: x => x.Order_pizzaId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPizza_Pizza_Pizza_orderId",
                        column: x => x.Pizza_orderId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPizza_Pizza_orderId",
                table: "OrderPizza",
                column: "Pizza_orderId");
        }
    }
}
