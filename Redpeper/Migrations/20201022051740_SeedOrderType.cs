using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class SeedOrderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO public.\"OrderTypes\" (\"Id\", \"Name\") VALUES (1, 'En Restaurante');\nINSERT INTO public.\"OrderTypes\" (\"Id\", \"Name\") VALUES (2, 'Para Llevar');\nINSERT INTO public.\"OrderTypes\" (\"Id\", \"Name\") VALUES (3, 'A domicilio');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
