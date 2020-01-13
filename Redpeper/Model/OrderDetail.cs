using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public double Qty { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
    }
}
