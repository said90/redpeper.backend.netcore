using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class ComboImage
    {
        public int Id { get; set; }
        public int ComboId { get; set; }
        public byte[] Image { get; set; }
    }
}
