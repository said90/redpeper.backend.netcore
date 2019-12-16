using Microsoft.EntityFrameworkCore;
using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyInvoice> SupplyInvoices { get; set; }
        public DbSet<SupplyInvoiceDetail> SupplyInvoiceDetails { get; set; }


    }
}
