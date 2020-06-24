using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingFieldsOnTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerLastName",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_CustomerId",
                table: "Tables",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Customers_CustomerId",
                table: "Tables",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Customers_CustomerId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_CustomerId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CustomerLastName",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Tables");
        }
    }
}
