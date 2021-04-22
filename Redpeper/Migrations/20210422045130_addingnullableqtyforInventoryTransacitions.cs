using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class addingnullableqtyforInventoryTransacitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "SupplyQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DishQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ComboQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SupplyQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DishQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComboQty",
                table: "InventorySupplyTransactions",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
