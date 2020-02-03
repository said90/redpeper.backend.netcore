using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Tables
{
    public interface ITableRepository
    {
        Task<List<Table>> GetAll();
        Task<Table> GetById(int id);
        Task<Table> GetByName(string name);
        void Create(Table table);
        void Update(Table table);
        void Remove(Table table);
    }
}
