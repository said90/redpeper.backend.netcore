using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class SupplyRepository : ISupplyRepository
    {
        private readonly DataContext _dataContext;

        public SupplyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Supply>> GetAll()
        {
            return await _dataContext.Supplies.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<PagedList<Supply>> GetPaginated(int pageNumber, int pageSize, string sort)
        {
            return await _dataContext.Supplies.ToPagedListAsync(pageNumber, pageSize, sort);
        }

        public async Task<Supply> GetById(int id)
        {
            return await _dataContext.Supplies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supply> GetByName(string name)
        {
            return await _dataContext.Supplies.FirstOrDefaultAsync(x => x.Name == name);
        }

        public void  Create(Supply supply)
        {
            _dataContext.Supplies.Add(supply);
        }

        public void Update(Supply supply)
        {
            _dataContext.Supplies.Update(supply);
        }

        public void Remove(Supply supply)
        {
            _dataContext.Supplies.Remove(supply);
        }
    }
}
