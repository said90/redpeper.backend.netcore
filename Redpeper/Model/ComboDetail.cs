using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Redpeper.Model
{
    public class ComboDetail
    {
        public int Id { get; set; }
        public int ComboId { get; set; }
        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }

    }
}
