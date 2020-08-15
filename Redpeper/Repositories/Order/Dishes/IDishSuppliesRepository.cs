using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishSuppliesRepository :IRepository<DishSupply>
    {
        Task<List<DishSupply>> GetByDishId(int dishId);
        Task<List<DishSupply>> GetByDishIdNoTracking(int dishId);

    }
}
