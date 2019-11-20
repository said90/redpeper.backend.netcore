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
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<FacturaCompra> FacturaCompras { get; set; }
        public DbSet<DetalleCompraFactura> DetalleCompraFacturas { get; set; }
        public DbSet<MovientosInventario> MovientosInventario { get; set; }
        public DbSet<Inventario> Inventario { get; set; }

    }
}
