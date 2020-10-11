using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public interface IComboImageRepository : IRepository<ComboImage>
    {
        Task<ComboImage> GetByComboId(int id);
    }
}
