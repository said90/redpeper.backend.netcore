using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class MovientosInventario
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public virtual Insumo Insumo { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoMoviento { get; set; }

    }
}