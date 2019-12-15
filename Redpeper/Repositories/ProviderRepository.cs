using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private DataContext _dataContext;

        public ProviderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<Provider>> GetAll()
        {
            return  await _dataContext.Providers.ToListAsync();
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
