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
    public class ProviderRepository :BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Provider>> GetAllOrderById()
        {
            return  await _entities.OrderBy(x => x.Id).ToListAsync();
        }

    }
}
