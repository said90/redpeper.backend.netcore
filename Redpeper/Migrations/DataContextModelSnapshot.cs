﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Redpeper.Data;

namespace Redpeper.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

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

                    b.HasIndex("DishId");

                    b.ToTable("ComboDetails");
                });

            modelBuilder.Entity("Redpeper.Model.ComboImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComboId");

                    b.Property<byte[]>("Image");

                    b.HasKey("Id");

                    b.ToTable("ComboImages");
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

            modelBuilder.Entity("Redpeper.Model.DishImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DishId");

                    b.Property<byte[]>("Image");

                    b.HasKey("Id");

                    b.ToTable("DishImages");
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

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

                    b.Property<int>("EmployeeId");

                    b.Property<string>("OrderNumber");

                    b.Property<string>("Status");

                    b.Property<int>("TableId");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Redpeper.Model.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ComboId");

                    b.Property<string>("Comments");

                    b.Property<double>("Discount");

                    b.Property<int?>("DishId");

                    b.Property<int>("OrderId");

                    b.Property<double>("Qty");

                    b.Property<string>("Status");

                    b.Property<double>("Total");

                    b.Property<double>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ComboId");

                    b.HasIndex("DishId");

                    b.HasIndex("OrderId");

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

                    b.Property<bool>("Iva");

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

                    b.Property<int>("Chairs");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("CustomerLastName");

                    b.Property<string>("CustomerName");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Redpeper.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PicturePath");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Redpeper.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Redpeper.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Redpeper.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Redpeper.Model.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.ComboDetail", b =>
                {
                    b.HasOne("Redpeper.Model.Combo")
                        .WithMany("ComboDetails")
                        .HasForeignKey("ComboId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Redpeper.Model.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
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

            modelBuilder.Entity("Redpeper.Model.Employee", b =>
                {
                    b.HasOne("Redpeper.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
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

                    b.HasOne("Redpeper.Model.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Redpeper.Model.OrderDetail", b =>
                {
                    b.HasOne("Redpeper.Model.Combo", "Combo")
                        .WithMany()
                        .HasForeignKey("ComboId");

                    b.HasOne("Redpeper.Model.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId");

                    b.HasOne("Redpeper.Model.Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
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

            modelBuilder.Entity("Redpeper.Model.Table", b =>
                {
                    b.HasOne("Redpeper.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Redpeper.Model.User", b =>
                {
                    b.HasOne("Redpeper.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });
#pragma warning restore 612, 618
        }
    }
}
