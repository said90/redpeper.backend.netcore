using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Dto
{
    public class InventoryAdjustmentDto
    {
        public DateTime Date { get; set; }
        public int SupplyId { get; set; }
        public double Qty { get; set; }
        public int TransactionType { get; set; }
        public string TransactionNumber { get; set; }
        public string Comments { get; set; }
    }
}
