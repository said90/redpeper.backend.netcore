﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Redpeper.Data;

namespace Redpeper.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191125025317_changingSexFieldOnPersonaTable")]
    partial class changingSexFieldOnPersonaTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Redpeper.Model.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PersonaId");

                    b.Property<int>("idPersona");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Redpeper.Model.DetalleCompraFactura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<int?>("FacturaCompraId");

                    b.Property<int>("IdFacturaCompra");

                    b.Property<int>("IdInsumo");

                    b.Property<int?>("InsumoId");

                    b.Property<double>("Precio");

                    b.Property<double>("Total");

                    b.Property<string>("UnidadMedida");

                    b.HasKey("Id");

                    b.HasIndex("FacturaCompraId");

                    b.HasIndex("InsumoId");

                    b.ToTable("DetalleCompraFacturas");
                });

            modelBuilder.Entity("Redpeper.Model.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PersonaId");

                    b.Property<int>("idPersona");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("Redpeper.Model.FacturaCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdProveedor");

                    b.Property<int?>("ProveedorId");

                    b.Property<int>("Total");

                    b.Property<DateTime>("fecha");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("FacturaCompras");
                });

            modelBuilder.Entity("Redpeper.Model.Insumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("NombreInsumo");

                    b.HasKey("Id");

                    b.ToTable("Insumos");
                });

            modelBuilder.Entity("Redpeper.Model.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdInsumo");

                    b.Property<int?>("InsumoId");

                    b.Property<double>("Stock");

                    b.HasKey("Id");

                    b.HasIndex("InsumoId");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("Redpeper.Model.MovientosInventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("IdInsumo");

                    b.Property<int?>("InsumoId");

                    b.Property<int>("TipoMoviento");

                    b.HasKey("Id");

                    b.HasIndex("InsumoId");

                    b.ToTable("MovientosInventario");
                });

            modelBuilder.Entity("Redpeper.Model.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("CorreoElectronico")
                        .HasMaxLength(100);

                    b.Property<string>("Direccion")
                        .HasMaxLength(200);

                    b.Property<string>("Dui")
                        .HasMaxLength(10);

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Nit")
                        .HasMaxLength(17);

                    b.Property<string>("Nombres");

                    b.Property<string>("Sexo");

                    b.Property<string>("Telefono")
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("Redpeper.Model.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NombreProveedor");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Redpeper.Model.Cliente", b =>
                {
                    b.HasOne("Redpeper.Model.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId");
                });

            modelBuilder.Entity("Redpeper.Model.DetalleCompraFactura", b =>
                {
                    b.HasOne("Redpeper.Model.FacturaCompra", "FacturaCompra")
                        .WithMany()
                        .HasForeignKey("FacturaCompraId");

                    b.HasOne("Redpeper.Model.Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("InsumoId");
                });

            modelBuilder.Entity("Redpeper.Model.Empleado", b =>
                {
                    b.HasOne("Redpeper.Model.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId");
                });

            modelBuilder.Entity("Redpeper.Model.FacturaCompra", b =>
                {
                    b.HasOne("Redpeper.Model.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");
                });

            modelBuilder.Entity("Redpeper.Model.Inventario", b =>
                {
                    b.HasOne("Redpeper.Model.Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("InsumoId");
                });

            modelBuilder.Entity("Redpeper.Model.MovientosInventario", b =>
                {
                    b.HasOne("Redpeper.Model.Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("InsumoId");
                });
#pragma warning restore 612, 618
        }
    }
}
