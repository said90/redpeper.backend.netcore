using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class InventorySupplyTransaction
    {
        public int Id { get; set; }
        public double Qty { get; set; }
        public int SupplyId { get; set; }
        public double? SupplyQty { get; set; }
        public int TransactionType { get; set; }
        public string TransactionNumber { get; set; }
        public virtual Supply Supply { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? OrderId { get; set; }
        public int? DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public double? DishQty { get; set; }
        public int? ComboId { get; set; }
        public virtual Combo Combo { get; set; }
        public double? ComboQty { get; set; }
        public string Comments { get; set; }
    }
}
