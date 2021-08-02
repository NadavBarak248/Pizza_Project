using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class pizzasinorder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzasInOrder",
                table: "PizzasInOrder");

            migrationBuilder.DropIndex(
                name: "IX_PizzasInOrder_PizzaId",
                table: "PizzasInOrder");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PizzasInOrder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzasInOrder",
                table: "PizzasInOrder",
                columns: new[] { "PizzaId", "OrderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzasInOrder",
                table: "PizzasInOrder");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PizzasInOrder",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzasInOrder",
                table: "PizzasInOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PizzasInOrder_PizzaId",
                table: "PizzasInOrder",
                column: "PizzaId");
        }
    }
}
