using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class FacturaCompra
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime fecha { get; set; }
        public int Total{ get; set; }


    }
}
