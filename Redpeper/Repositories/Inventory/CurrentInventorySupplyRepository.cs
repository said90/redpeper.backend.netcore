using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public class CurrentInventorySupplyRepository : ICurrentInventorySupplyRepository
    {
        private readonly DataContext _dataContext;

        public CurrentInventorySupplyRepository(DataContext dataContext) {
            _dataContext = dataContext;
        }

        public Task<List<CurrentInventorySupply>> GetActualInventoryBySupply(int supplyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CurrentInventorySupply>> GetAllActualInventory()
        {

            return _dataContext.CurrentInventorySupplies.GroupBy(x => x.SupplyId).Select(y => new CurrentInventorySupply
            {
                Date =y.First().Date,
                Id = y.First().Id,
                SupplyId = y.First().SupplyId,
                Supply = y.First().Supply,
                Qty = y.Sum(z=>z.Qty)
            }).ToListAsync();
            
        }

        public Task<List<CurrentInventorySupply>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<CurrentInventorySupply>> GetByExpirationDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<CurrentInventorySupply> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(CurrentInventorySupply currentInventorySupply)
        {
            _dataContext.CurrentInventorySupplies.Add(currentInventorySupply);
        }

        public  void CreateRange(List<CurrentInventorySupply> currentInventorySupply)
        {
             _dataContext.CurrentInventorySupplies.AddRange(currentInventorySupply);
        }

        public void Update(CurrentInventorySupply currentInventorySupply)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<CurrentInventorySupply> currentInventorySupply)
        {
            throw new NotImplementedException();
        }
        public void Remove(CurrentInventorySupply currentInventorySupply)
        {
            throw new NotImplementedException();
        }

    }
}
