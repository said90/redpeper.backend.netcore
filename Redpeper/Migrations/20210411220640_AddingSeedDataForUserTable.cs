using Microsoft.EntityFrameworkCore.Migrations;

namespace Redpeper.Migrations
{
    public partial class AddingSeedDataForUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                  INSERT INTO public.""AspNetRoles"" (""Id"", ""Name"", ""NormalizedName"", ""ConcurrencyStamp"") VALUES ('a446b3ca-a5ed-4d2f-a524-9e3faef414ea', 'Admin', 'ADMIN', 'af26e630-24e9-4ec1-b012-cd9ace85c595');
                  INSERT INTO public.""AspNetRoles"" (""Id"", ""Name"", ""NormalizedName"", ""ConcurrencyStamp"") VALUES ('b263c7f0-4926-4973-80e5-0b1f4d5e14da', 'Mesero', 'MESERO', 'fb11a849-e1c7-4ef2-b002-0853aebf2381'); 
                  INSERT INTO public.""AspNetUsers"" VALUES ('7eec22e7-c24a-47a6-97d5-9595399a1d99', 'mesero', 'MESERO', 'mesero@gmail.com', 'MESERO@GMAIL.COM', false, 'AQAAAAEAACcQAAAAEF/49fn9kedJ38vKYgwwIjiL6OvODK754fKs1G3CIKAQIqrgBhdremswKF45LsCjLw==', 'WJTYPSOKBHVGKNIK6ALGQVRPDOI6QQEA', '5c90a0f8-6290-4b44-8586-50ab603df646', NULL, false, false, NULL, true, 0, 'mesero', 'mesero', NULL, NULL, 1);
                  INSERT INTO public.""AspNetUsers"" VALUES ('e994e443-c57a-428c-af44-87289565904f', 'admin', 'ADMIN', 'admin@gmail.com', 'ADMIN@GMAIL.COM', false, 'AQAAAAEAACcQAAAAEOmJM9oNrz2g7xlCWis6ZfSoe7rYirzp7MQSFZFZBSVnk9ajVblkiyoyN8uPguDevg==', 'FNE7OZNQOVEMYIW2FI2KUTJ5L4Q6SBKS', '023cc4e7-16bd-4e83-9911-adff3a08303d', NULL, false, false, NULL, true, 0, 'admin', 'admin', NULL, NULL, 0);
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}