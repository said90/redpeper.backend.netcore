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
    [Migration("20200219021249_AddingRelationWithOrder")]
    partial class AddingRelationWithOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Redpeper.Model.Combo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<double>("Total");

                    b.HasKey("Id");

                    b.ToTable("Combos");
                });

            modelBuilder.Entity("Redpeper.Model.ComboDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComboId");

                    b.Property<int>("DishId");

                    b.Property<double>("Price");

                    b.Property<double>("Qty");

                    b.HasKey("Id");

                    b.HasIndex("ComboId");

                    b.ToTable("ComboDetails");
                });

            modelBuilder.Entity("Redpeper.Model.CurrentInventorySupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<double>("Qty");

                    b.Property<int>("SupplyId");

                    b.HasKey("Id");

                    b.HasIndex("SupplyId");

                    b.ToTable("CurrentInventorySupplies");
                });

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

            modelBuilder.Entity("Redpeper.Model.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("DishCategoryId");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Redpeper.Model.DishCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DishCategories");
                });

            modelBuilder.Entity("Redpeper.Model.DishSupply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("DishId");

                    b.Property<double>("Qty");

                    b.Property<int>("SupplyId");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.ToTable("DishSupplies");
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

            modelBuilder.Entity("Redpeper.Model.InventorySupplyTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<double>("Qty");

                    b.Property<int>("SupplyId");

                    b.HasKey("Id");

                    b.HasIndex("SupplyId");

                    b.ToTable("InventorySupplyTransactions");
                });

            modelBuilder.Entity("Redpeper.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("OrderNumber");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Redpeper.Model.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Discount");

                    b.Property<int>("DishId");

                    b.Property<int>("OrderId");

                    b.Property<double>("Qty");

                    b.Property<string>("Status");

                    b.Property<double>("Total");

                    b.Property<double>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Redpeper.Model.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

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

                    b.Property<double>("MinimumQty");

                    b.Property<string>("Name");

                    b.Property<string>("Presentation");

                    b.Property<string>("UnitOfMeasure");

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

                    b.HasIndex("SupplyInvoiceId");

                    b.ToTable("SupplyInvoiceDetails");
                });

            modelBuilder.Entity("Redpeper.Model.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Chairs");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Redpeper.Model.ComboDetail", b =>
                {
                    b.HasOne("Redpeper.Model.Combo")
                        .WithMany("ComboDetails")
                        .HasForeignKey("ComboId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.CurrentInventorySupply", b =>
                {
                    b.HasOne("Redpeper.Model.Supply", "Supply")
                        .WithMany()
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.DishSupply", b =>
                {
                    b.HasOne("Redpeper.Model.Dish")
                        .WithMany("DishSupplies")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.InventorySupplyTransaction", b =>
                {
                    b.HasOne("Redpeper.Model.Supply", "Supply")
                        .WithMany()
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.Order", b =>
                {
                    b.HasOne("Redpeper.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.OrderDetail", b =>
                {
                    b.HasOne("Redpeper.Model.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("Redpeper.Model.SupplyInvoice")
                        .WithMany("Details")
                        .HasForeignKey("SupplyInvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
