using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Dto;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public class CurrentInventorySupplyRepository : BaseRepository<CurrentInventorySupply>,
        ICurrentInventorySupplyRepository
    {
        public CurrentInventorySupplyRepository(DataContext dataContext) : base(dataContext)
        {
        }


        public Task<List<CurrentInventorySupply>> GetActualInventoryBySupply(int supplyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<InventoryDto>> GetAllActualInventory()
        {
            return _dataContext.InventorySupplyTransactions.GroupBy(x => x.SupplyId).Select(y => new InventoryDto
            {
                Supply = y.First().Supply.Name,
                Qty = y.Sum(z => z.Qty)
            }).OrderBy(x => x.Supply).ToListAsync();
        }

        public Task<List<InventoryDto>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dataContext.InventorySupplyTransactions.Where(x => x.Date >= startDate && x.Date <= endDate).GroupBy(x => x.SupplyId)
                .Select(y => new InventoryDto
                {
                    Supply = y.First().Supply.Name,
                    Qty = y.Sum(z => z.Qty)
                }).ToListAsync();
        }

        public Task<List<CurrentInventorySupply>> GetByExpirationDateRange(DateTime startDate, DateTime endDate)
        {
            return _entities.GroupBy(x => x.SupplyId).Select(y => new CurrentInventorySupply
            {
                Date = y.First().Date,
                Id = y.First().Id,
                SupplyId = y.First().SupplyId,
                Supply = y.First().Supply,
                Qty = y.Sum(z => z.Qty)
            }).Where(x => x.ExpirationDate >= startDate && x.ExpirationDate <= endDate).ToListAsync();
        }
    }
}