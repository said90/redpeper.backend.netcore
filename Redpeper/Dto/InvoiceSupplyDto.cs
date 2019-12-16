using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class InvoiceSupplyDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime EmissionDate { get; set; }
        public int ProviderId { get; set; }
        public double Total { get; set; }
        public  List<SupplyInvoiceDetail> Details { get; set; }
    }
}
