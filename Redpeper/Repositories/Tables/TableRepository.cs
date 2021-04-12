using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories.Tables
{
    public class TableRepository : BaseRepository<Table> , ITableRepository
    {
        public TableRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Table>> GetAllInludingClients()
        {
            return await _entities.Include(x=>x.Customer).OrderBy(x=>x.Id).ToListAsync();
        }


        public async Task<List<Table>> GetByIdRange(List<int?> ids)
        {
            return await _entities.Where(x => ids.Contains(x.Id)).ToListAsync();

        }

        public async Task<Table> GetByName(string name)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}
