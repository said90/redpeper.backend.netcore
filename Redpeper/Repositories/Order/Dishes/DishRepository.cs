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
    public class DishRepository :BaseRepository<Dish>, IDishRepository
    {
        public DishRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<List<Dish>> GetAllIncludingSuppliesTask()
        {
            return await  _entities.Include(x=>x.DishSupplies).OrderBy(x => x.Id).ToListAsync();
        }


        public async Task<Dish> GetByIdIncludeSuppliesTask(int id)
        {
            return await _entities.Include(x=>x.DishSupplies).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Dish> GetByName(string name)
        {
            return await _entities.Include(x => x.DishSupplies).FirstOrDefaultAsync(x => x.Name == name);
        }

       }
}
