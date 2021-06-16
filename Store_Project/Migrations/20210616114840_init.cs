using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Pizza_size = table.Column<int>(type: "int", nullable: false),
                    Pizza_width = table.Column<int>(type: "int", nullable: false),
                    Pizza_sauce = table.Column<int>(type: "int", nullable: false),
                    With_cheese = table.Column<bool>(type: "bit", nullable: false),
                    To_present = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Slice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    Orders_number = table.Column<double>(type: "float", nullable: false)
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
                name: "PizzaTag",
                columns: table => new
                {
                    Pizza_tagId = table.Column<int>(type: "int", nullable: false),
                    Pizza_tagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTag", x => new { x.Pizza_tagId, x.Pizza_tagsId });
                    table.ForeignKey(
                        name: "FK_PizzaTag_Pizza_Pizza_tagId",
                        column: x => x.Pizza_tagId,
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTag_Tag_Pizza_tagsId",
                        column: x => x.Pizza_tagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_orderId = table.Column<int>(type: "int", nullable: true),
                    Order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expected_delivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time_delivered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_User_orderId",
                        column: x => x.User_orderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Order_User_orderId",
                table: "Order",
                column: "User_orderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPizza_Pizza_orderId",
                table: "OrderPizza",
                column: "Pizza_orderId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaImage_PizzaId",
                table: "PizzaImage",
                column: "PizzaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTag_Pizza_tagsId",
                table: "PizzaTag",
                column: "Pizza_tagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Slice_PizzaId",
                table: "Slice",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_SliceTopping_toppingsSlicesId",
                table: "SliceTopping",
                column: "toppingsSlicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPizza");

            migrationBuilder.DropTable(
                name: "PizzaImage");

            migrationBuilder.DropTable(
                name: "PizzaTag");

            migrationBuilder.DropTable(
                name: "SliceTopping");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Slice");

            migrationBuilder.DropTable(
                name: "Topping");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Pizza");
        }
    }
}
