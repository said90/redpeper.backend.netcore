using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingFieldCommentToOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "OrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "OrderDetails");
        }
    }
}
