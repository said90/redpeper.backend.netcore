using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Dto
{
    public class InventoryTransactionDetails
    {
        public List<InventoryTransactionDto> InventoryTransactions { get; set; }
        public double Total { get; set; }
    }
}
