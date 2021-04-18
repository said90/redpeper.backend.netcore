﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class InventorySupplyTransaction
    {
        public int Id { get; set; }
        public double Qty { get; set; }
        public int SupplyId { get; set; }
        public int TransactionType { get; set; }
        public string TransactionNumber { get; set; }
        public virtual Supply Supply { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Comments { get; set; }
    }
}
