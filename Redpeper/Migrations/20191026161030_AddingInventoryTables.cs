using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Redpeper.Migrations
{
    public partial class AddingInventoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Personas_personaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Personas_personaId",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "personaId",
                table: "Empleados",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_personaId",
                table: "Empleados",
                newName: "IX_Empleados_PersonaId");

            migrationBuilder.RenameColumn(
                name: "personaId",
                table: "Clientes",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_personaId",
                table: "Clientes",
                newName: "IX_Clientes_PersonaId");

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NombreInsumo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NombreProveedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdInsumo = table.Column<int>(nullable: false),
                    InsumoId = table.Column<int>(nullable: true),
                    Stock = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventario_Insumos_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovientosInventario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdInsumo = table.Column<int>(nullable: false),
                    InsumoId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TipoMoviento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovientosInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovientosInventario_Insumos_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaCompras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdProveedor = table.Column<int>(nullable: false),
                    ProveedorId = table.Column<int>(nullable: true),
                    fecha = table.Column<DateTime>(nullable: false),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaCompras_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompraFacturas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdFacturaCompra = table.Column<int>(nullable: false),
                    FacturaCompraId = table.Column<int>(nullable: true),
                    IdInsumo = table.Column<int>(nullable: false),
                    InsumoId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    UnidadMedida = table.Column<string>(nullable: true),
                    Precio = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompraFacturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompraFacturas_FacturaCompras_FacturaCompraId",
                        column: x => x.FacturaCompraId,
                        principalTable: "FacturaCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleCompraFacturas_Insumos_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompraFacturas_FacturaCompraId",
                table: "DetalleCompraFacturas",
                column: "FacturaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompraFacturas_InsumoId",
                table: "DetalleCompraFacturas",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaCompras_ProveedorId",
                table: "FacturaCompras",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_InsumoId",
                table: "Inventario",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovientosInventario_InsumoId",
                table: "MovientosInventario",
                column: "InsumoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Personas_PersonaId",
                table: "Clientes",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Personas_PersonaId",
                table: "Empleados",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Personas_PersonaId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Personas_PersonaId",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "DetalleCompraFacturas");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "MovientosInventario");

            migrationBuilder.DropTable(
                name: "FacturaCompras");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Empleados",
                newName: "personaId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_PersonaId",
                table: "Empleados",
                newName: "IX_Empleados_personaId");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Clientes",
                newName: "personaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_PersonaId",
                table: "Clientes",
                newName: "IX_Clientes_personaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Personas_personaId",
                table: "Clientes",
                column: "personaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Personas_personaId",
                table: "Empleados",
                column: "personaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
