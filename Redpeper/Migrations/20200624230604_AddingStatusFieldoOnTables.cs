using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingStatusFieldoOnTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastName",
                table: "Tables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Tables",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_UserId1",
                table: "Tables",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_AspNetUsers_UserId1",
                table: "Tables",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_AspNetUsers_UserId1",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_UserId1",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserLastName",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Tables");
        }
    }
}
