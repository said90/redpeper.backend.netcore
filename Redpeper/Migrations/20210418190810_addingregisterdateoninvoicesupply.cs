using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class addingregisterdateoninvoicesupply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "SupplyInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "SupplyInvoices");
        }
    }
}
