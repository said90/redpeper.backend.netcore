using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class combomigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboDetails_Dishes_DishId",
                table: "ComboDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DishSupplies_Supplies_SupplyId",
                table: "DishSupplies");

            migrationBuilder.DropIndex(
                name: "IX_DishSupplies_SupplyId",
                table: "DishSupplies");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_DishCategoryId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_ComboDetails_DishId",
                table: "ComboDetails");

            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "ComboDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetails_ComboId",
                table: "ComboDetails",
                column: "ComboId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboDetails_Combos_ComboId",
                table: "ComboDetails",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboDetails_Combos_ComboId",
                table: "ComboDetails");

            migrationBuilder.DropIndex(
                name: "IX_ComboDetails_ComboId",
                table: "ComboDetails");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "ComboDetails");

            migrationBuilder.CreateIndex(
                name: "IX_DishSupplies_SupplyId",
                table: "DishSupplies",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboDetails_DishId",
                table: "ComboDetails",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboDetails_Dishes_DishId",
                table: "ComboDetails",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishCategories_DishCategoryId",
                table: "Dishes",
                column: "DishCategoryId",
                principalTable: "DishCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DishSupplies_Supplies_SupplyId",
                table: "DishSupplies",
                column: "SupplyId",
                principalTable: "Supplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
