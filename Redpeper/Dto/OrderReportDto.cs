using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class OrderReportDto
    {
        public DateTime Date { get; set; }
        public string OrderType { get; set; }
        public string OrderNumber { get; set; }
        public string  Customer { get; set; }
        public Decimal Total { get; set; }
        public bool Tip { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
