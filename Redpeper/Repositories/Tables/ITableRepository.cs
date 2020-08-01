using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Tables
{
    public interface ITableRepository
    {
        Task<List<Table>> GetAll();

        Task<PagedList<Table>> GetPaginated(int pageNumber, int pageSize, string sort);

        Task<Table> GetById(int id);
        Task<List<Table>> GetByIdRange(List<int> ids);
        Task<Table> GetByName(string name);
        void Create(Table table);
        void Update(Table table);
        void Remove(Table table);
    }
}
