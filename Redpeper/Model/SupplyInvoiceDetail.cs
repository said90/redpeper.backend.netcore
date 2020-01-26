using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class SupplyInvoiceDetail
    {
        public int Id { get; set; }
        public int SupplyInvoiceId { get; set; }
        public int SupplyId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
