using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Repositories.Inventory
{
    public interface ICurrentInventorySupplyRepository
    {
        Task<List<CurrentInventorySupply>> GetAllActualInventory();
        Task<List<CurrentInventorySupply>> GetActualInventoryBySupply(int supplyId);
        Task<CurrentInventorySupply> GetById(int id);
        Task<List<CurrentInventorySupply>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<List<CurrentInventorySupply>> GetByExpirationDateRange(DateTime startDate, DateTime endDate);
        void Create(CurrentInventorySupply currentInventorySupply);
        void CreateRange(List<CurrentInventorySupply> currentInventorySupply);
        void Update(CurrentInventorySupply currentInventorySupply);
        void UpdateRange(List<CurrentInventorySupply> currentInventorySupply);
        void Remove(CurrentInventorySupply currentInventorySupply);       

    }
}
