using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public class DishSuppliesRepository :BaseRepository<DishSupply>, IDishSuppliesRepository
    {
        public DishSuppliesRepository(DataContext dataContext) : base(dataContext)
        {
        }
        
        public async Task<List<DishSupply>> GetByDishId(int dishId)
        {
            return await _entities.Where(x => x.DishId == dishId).ToListAsync();
        }

        public async Task<List<DishSupply>> GetByDishIdNoTracking(int dishId)
        {
            return await _entities.Where(x => x.DishId == dishId).AsNoTracking().ToListAsync();
        }
        
    }
}

