using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public int idPersona { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
