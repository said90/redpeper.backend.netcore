using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public interface IComboRepository
    {
        Task<List<Combo>> GetAll();
        Task<PagedList<Combo>> GetPaginated(int pageNumber, int pageSize, string sort);

        Task<Combo> GetById(int id);
        Task<Combo> GetByName(string name);
        Task<int> GetMaxCombo();
        void Create(Combo combo);
        void Update(Combo combo);
        void Remove(Combo combo);
    }
}
