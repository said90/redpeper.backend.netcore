using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public class DishCategoryRepository :BaseRepository<DishCategory>, IDishCategoryRepository
    {
        public DishCategoryRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<DishCategory>> GetAllOrderBy()
        {
            return await _entities.OrderBy(x=> x.Id).ToListAsync();
        }


        public async Task<DishCategory> GetByName(string name)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Name == name);
        }
        
    }
}
