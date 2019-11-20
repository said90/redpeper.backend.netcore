using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class DetalleCompraFactura
    {
        public int Id { get; set; }
        public int IdFacturaCompra { get; set; }
        public virtual FacturaCompra FacturaCompra { get; set; }
        public int IdInsumo { get; set; }
        public virtual Insumo Insumo { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }
    }
}
