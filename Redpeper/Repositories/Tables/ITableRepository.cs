using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Tables
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<List<Table>> GetAllInludingClients();
        Task<List<Table>> GetByIdRange(List<int?> ids);
        Task<Table> GetByName(string name);
    }
}
