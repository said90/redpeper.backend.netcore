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
    public class SupplyRepository :BaseRepository<Supply>,ISupplyRepository
    {
        public SupplyRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Supply>> GetAllOrderById()
        {
            return await _entities.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Supply> GetByName(string name)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Name == name);
        }
       
    }
}
