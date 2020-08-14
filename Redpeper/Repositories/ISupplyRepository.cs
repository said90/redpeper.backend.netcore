using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface ISupplyRepository : IRepository<Supply>
    {
        Task<List<Supply>> GetAllOrderById();
        Task<Supply> GetByName(string name);
    }
}
