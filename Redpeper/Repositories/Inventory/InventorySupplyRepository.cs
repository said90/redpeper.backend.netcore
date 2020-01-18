using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public class InventorySupplyRepository : IInventorySupplyTransactionRepository
    {
        private readonly DataContext _dataContext;

        public InventorySupplyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateRange(List<InventorySupplyTransaction> inventorySupplyTransactions)
        {
             _dataContext.InventorySupplyTransactions.AddRange(inventorySupplyTransactions); 
        }
    }
}
