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
    public class ProviderRepository : IProviderRepository
    {
        private readonly DataContext _dataContext;

        public ProviderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<Provider>> GetAll()
        {
            return  await _dataContext.Providers.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<PagedList<Provider>> GetPaginated(int pageNumber, int pageSize)
        {
            var result =await _dataContext.Providers.ToPagedListAsync(pageNumber, pageSize);
            return await _dataContext.Providers.ToPagedListAsync(pageNumber,pageSize);
        }

        public async Task<Provider> GetById(int id)

        {
            return await _dataContext.Providers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Create(Provider provider)
        {
            _dataContext.Providers.Add(provider);
        }

        public void Update(Provider provider)
        {
            _dataContext.Providers.Update(provider);
        }

        public void Remove(Provider provider)
        {
            _dataContext.Providers.Remove(provider);
        }
    }
}
