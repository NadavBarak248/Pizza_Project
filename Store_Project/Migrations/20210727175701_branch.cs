using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class branch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "branch_Idid",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_branch_Idid",
                table: "Order",
                column: "branch_Idid");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Branch_branch_Idid",
                table: "Order",
                column: "branch_Idid",
                principalTable: "Branch",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Branch_branch_Idid",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropIndex(
                name: "IX_Order_branch_Idid",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "branch_Idid",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
