using Microsoft.EntityFrameworkCore;
using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Redpeper.Data
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboImage> ComboImages { get; set; }
        public DbSet<ComboDetail> ComboDetails { get; set; }
        public DbSet<CurrentInventorySupply> CurrentInventorySupplies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishImage> DishImages { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<DishSupply> DishSupplies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventorySupplyTransaction> InventorySupplyTransactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyInvoice> SupplyInvoices { get; set; }
        public DbSet<SupplyInvoiceDetail> SupplyInvoiceDetails { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
