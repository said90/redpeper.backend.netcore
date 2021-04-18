using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Dto
{
    public class InventoryTransactionDto
    {
        public int  Id { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
        public string TransationNumber { get; set; }
        public string Supply { get; set; }
        public double Qty { get; set; }
        public string Comments { get; set; }
    }
}
