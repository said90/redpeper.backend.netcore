using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public class DishImageRepository : BaseRepository<DishImage>, IDishImageRepository
    {
        public DishImageRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<DishImage> GetByDishId(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.DishId == id);
        }
    }
}
