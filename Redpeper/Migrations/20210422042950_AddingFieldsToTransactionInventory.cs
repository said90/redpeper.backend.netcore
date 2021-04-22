using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingFieldsToTransactionInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComboQty",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DishQty",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplyQty",
                table: "InventorySupplyTransactions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComboQty",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "DishQty",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "SupplyQty",
                table: "InventorySupplyTransactions");
        }
    }
}
