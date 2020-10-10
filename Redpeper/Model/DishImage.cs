using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class DishImage
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public byte[] Image { get; set; }
    }
}
