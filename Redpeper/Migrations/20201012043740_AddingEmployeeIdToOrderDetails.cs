using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingEmployeeIdToOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DishImages_DishId",
                table: "DishImages",
                column: "DishId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboImages_ComboId",
                table: "ComboImages",
                column: "ComboId",
                unique: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_DishImages_DishId",
                table: "DishImages");

            migrationBuilder.DropIndex(
                name: "IX_ComboImages_ComboId",
                table: "ComboImages");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "OrderDetails");
        }
    }
}
