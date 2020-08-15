using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public interface IComboRepository: IRepository<Combo>
    {
        Task<List<Combo>> GetAllInludeDetails();
        Task<Combo> GetByName(string name);

    }
}
