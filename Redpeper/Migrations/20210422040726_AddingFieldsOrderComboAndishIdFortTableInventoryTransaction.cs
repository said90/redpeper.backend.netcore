using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingFieldsOrderComboAndishIdFortTableInventoryTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DishId",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventorySupplyTransactions_ComboId",
                table: "InventorySupplyTransactions",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_InventorySupplyTransactions_DishId",
                table: "InventorySupplyTransactions",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventorySupplyTransactions_Combos_ComboId",
                table: "InventorySupplyTransactions",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventorySupplyTransactions_Dishes_DishId",
                table: "InventorySupplyTransactions",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventorySupplyTransactions_Combos_ComboId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventorySupplyTransactions_Dishes_DishId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventorySupplyTransactions_ComboId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropIndex(
                name: "IX_InventorySupplyTransactions_DishId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "InventorySupplyTransactions");
        }
    }
}
