using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Inventario
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public Insumo Insumo { get; set; }
        public double Stock { get; set; }
    }
}
