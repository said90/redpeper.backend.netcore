using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Redpeper.Model
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order{ get; set; }
        public int? DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public int? ComboId { get; set; }
        public virtual Combo Combo{ get; set; }
        public double Qty { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public string Comments { get; set; }
        public double Total { get; set; }
        public string Status { get; set; }
    }
}
