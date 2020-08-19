using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public class InventorySupplyTransactionRepository :BaseRepository<InventorySupplyTransaction>, IInventorySupplyTransactionRepository
    {
        public InventorySupplyTransactionRepository(DataContext dataContext) : base(dataContext)
        {
        }
   
    }
}
