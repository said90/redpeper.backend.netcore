using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderTypeId { get; set; }
        [ForeignKey("OrderTypeId")]
        public virtual OrderType OrderType { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? TableId { get; set; }
        public virtual Table Table { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal Total { get; set; }
        public string Status { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public int EmployeeId { get; set; }
        public string NotificationToken { get; set; }

    }
}
