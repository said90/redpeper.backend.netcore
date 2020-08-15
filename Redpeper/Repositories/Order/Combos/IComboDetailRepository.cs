using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public interface IComboDetailRepository: IRepository<ComboDetail>
    {
        Task<List<ComboDetail>> GetDetailsByCombo(int comboId);
        Task<List<ComboDetail>> GetDetailsByComboNoTracking(int comboId);
    }
}
