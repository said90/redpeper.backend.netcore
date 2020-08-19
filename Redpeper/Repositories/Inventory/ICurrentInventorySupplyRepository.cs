using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Repositories.Inventory
{
    public interface ICurrentInventorySupplyRepository : IRepository<CurrentInventorySupply>
    {
        Task<List<CurrentInventorySupply>> GetAllActualInventory();
        Task<List<CurrentInventorySupply>> GetActualInventoryBySupply(int supplyId);
        Task<List<CurrentInventorySupply>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<List<CurrentInventorySupply>> GetByExpirationDateRange(DateTime startDate, DateTime endDate);
     

    }
}
