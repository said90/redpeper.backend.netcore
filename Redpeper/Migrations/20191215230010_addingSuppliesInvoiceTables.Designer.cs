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
    [Migration("20191215230010_addingSuppliesInvoiceTables")]
    partial class addingSuppliesInvoiceTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Redpeper.Model.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Dui");

                    b.Property<string>("Lastname");

                    b.Property<string>("Name");

                    b.Property<string>("Nit");

                    b.Property<string>("Sex");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Redpeper.Model.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Dui");

                    b.Property<string>("Email");

                    b.Property<string>("Lastname");

                    b.Property<string>("Name");

                    b.Property<string>("Nit");

                    b.Property<string>("Sex");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Redpeper.Model.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Redpeper.Model.Supply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Supplies");
                });

            modelBuilder.Entity("Redpeper.Model.SupplyInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EmissionDate");

                    b.Property<string>("InvoiceNumber");

                    b.Property<int>("ProviderId");

                    b.Property<double>("Total");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("SupplyInvoices");
                });

            modelBuilder.Entity("Redpeper.Model.SupplyInvoiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<double>("Quantity");

                    b.Property<int>("SupplyId");

                    b.Property<int>("SupplyInvoiceId");

                    b.Property<double>("Total");

                    b.Property<double>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("SupplyId");

                    b.HasIndex("SupplyInvoiceId");

                    b.ToTable("SupplyInvoiceDetails");
                });

            modelBuilder.Entity("Redpeper.Model.SupplyInvoice", b =>
                {
                    b.HasOne("Redpeper.Model.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.SupplyInvoiceDetail", b =>
                {
                    b.HasOne("Redpeper.Model.Supply", "Supply")
                        .WithMany()
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Redpeper.Model.SupplyInvoice", "SupplyInvoice")
                        .WithMany()
                        .HasForeignKey("SupplyInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
