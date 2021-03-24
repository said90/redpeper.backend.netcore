using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;

namespace Redpeper.Repositories.Inventory
{
    public interface ICurrentInventorySupplyRepository : IRepository<CurrentInventorySupply>
    {
        Task<List<InventoryDto>> GetAllActualInventory();
        Task<List<CurrentInventorySupply>> GetActualInventoryBySupply(int supplyId);
        Task<List<CurrentInventorySupply>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<List<CurrentInventorySupply>> GetByExpirationDateRange(DateTime startDate, DateTime endDate);
     

    }
}
