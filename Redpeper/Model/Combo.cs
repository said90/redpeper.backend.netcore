using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Combo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }
        public virtual List<ComboDetail> ComboDetails { get; set; }
    }
}
