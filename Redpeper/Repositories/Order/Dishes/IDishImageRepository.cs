using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishImageRepository : IRepository<DishImage>
    {
        Task<DishImage> GetByDishId(int id);
    }
}
