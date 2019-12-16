using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface ISupplyRepository
    {
        Task<List<Supply>> GetAll();
        Task<Supply> GetById(int id);
        Task<Supply> GetByName(string name);
        void Create(Supply supply);
        void Update(Supply supply);
        void Remove(Supply supply);
    }
}
