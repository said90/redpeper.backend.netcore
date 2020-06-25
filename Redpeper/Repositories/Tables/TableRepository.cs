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
    public class TableRepository : ITableRepository
    {
        private DataContext _dataContext;

        public TableRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Table>> GetAll()
        {
            return await _dataContext.Tables.Include(x=>x.Customer).OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<PagedList<Table>> GetPaginated(int pageNumber, int pageSize, string sort)
        {
            return await _dataContext.Tables.ToPagedListAsync(pageNumber, pageSize, sort);
        }

        public async Task<Table> GetById(int id)
        {
            return await _dataContext.Tables.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Table> GetByName(string name)
        {
            return await _dataContext.Tables.FirstOrDefaultAsync(x => x.Name == name);
        }

        public void Create(Table table)
        {
            _dataContext.Tables.Add(table);
        }

        public void Update(Table table)
        {
            _dataContext.Tables.Update(table);
        }

        public void Remove(Table table)
        {
            _dataContext.Tables.Remove(table);
        }
    }
}
