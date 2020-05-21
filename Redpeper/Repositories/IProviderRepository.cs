using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface IProviderRepository
    {
        Task<List<Provider>> GetAll();
        Task<PagedList<Provider>> GetPaginated(int pageNumber, int pageSize);
        Task<Provider> GetById(int id);
        void Create(Provider provider);
        void Update(Provider provider);
        void Remove(Provider provider);
    }
}
