using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class changingSexFieldOnPersonaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Personas",
                nullable: true,
                oldClrType: typeof(char));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Sexo",
                table: "Personas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
