using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishCategoryRepository : IRepository<DishCategory>
    {
        Task<List<DishCategory>> GetAllOrderBy();
        Task<DishCategory> GetByName(string name);
  
    }
}
