using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string Status { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
