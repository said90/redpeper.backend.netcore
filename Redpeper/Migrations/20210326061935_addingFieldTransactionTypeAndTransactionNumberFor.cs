using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class addingFieldTransactionTypeAndTransactionNumberFor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "InventorySupplyTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "InventorySupplyTransactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionNumber",
                table: "CurrentInventorySupplies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "CurrentInventorySupplies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "InventorySupplyTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "CurrentInventorySupplies");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "CurrentInventorySupplies");
        }
    }
}
