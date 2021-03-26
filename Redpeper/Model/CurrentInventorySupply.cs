using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class CurrentInventorySupply
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int  SupplyId { get; set; }
        public int TransactionType { get; set; }
        public string TransactionNumber { get; set; }
        public virtual Supply Supply { get; set; }
        public double Qty { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
