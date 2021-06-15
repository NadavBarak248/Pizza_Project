using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class slicestopping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topping_Slice_SliceId",
                table: "Topping");

            migrationBuilder.DropIndex(
                name: "IX_Topping_SliceId",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "SliceId",
                table: "Topping");

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
                name: "IX_SliceTopping_toppingsSlicesId",
                table: "SliceTopping",
                column: "toppingsSlicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliceTopping");

            migrationBuilder.AddColumn<int>(
                name: "SliceId",
                table: "Topping",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topping_SliceId",
                table: "Topping",
                column: "SliceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topping_Slice_SliceId",
                table: "Topping",
                column: "SliceId",
                principalTable: "Slice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
