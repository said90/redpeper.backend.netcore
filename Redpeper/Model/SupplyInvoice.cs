using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class SupplyInvoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime EmissionDate { get; set; }
        public virtual Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public double Total { get; set; }
        public virtual List<SupplyInvoiceDetail> Details { get; set; }

    }
}
