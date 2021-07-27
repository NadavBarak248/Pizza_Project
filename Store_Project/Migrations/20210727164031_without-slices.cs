using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class withoutslices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliceTopping");

            migrationBuilder.DropTable(
                name: "Slice");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Slice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orders_number = table.Column<double>(type: "float", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slice_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SliceTopping",
                columns: table => new
                {
                    ToppingsId = table.Column<int>(type: "int", nullable: false),
                    toppingsSlicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliceTopping", x => new { x.ToppingsId, x.toppingsSlicesId });
                    table.ForeignKey(
                        name: "FK_SliceTopping_Slice_toppingsSlicesId",
                        column: x => x.toppingsSlicesId,
                        principalTable: "Slice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SliceTopping_Topping_ToppingsId",
                        column: x => x.ToppingsId,
                        principalTable: "Topping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slice_PizzaId",
                table: "Slice",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_SliceTopping_toppingsSlicesId",
                table: "SliceTopping",
                column: "toppingsSlicesId");
        }
    }
}
