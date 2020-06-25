using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class changingdatatypeonchairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Chairs",
                table: "Tables",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Chairs",
                table: "Tables",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
