using Microsoft.EntityFrameworkCore.Migrations;

namespace Store_Project.Migrations
{
    public partial class branchorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Branch_branch_Idid",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "branch_Idid",
                table: "Order",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_branch_Idid",
                table: "Order",
                newName: "IX_Order_BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Branch_BranchId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Order",
                newName: "branch_Idid");

            migrationBuilder.RenameIndex(
                name: "IX_Order_BranchId",
                table: "Order",
                newName: "IX_Order_branch_Idid");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Branch_branch_Idid",
                table: "Order",
                column: "branch_Idid",
                principalTable: "Branch",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
