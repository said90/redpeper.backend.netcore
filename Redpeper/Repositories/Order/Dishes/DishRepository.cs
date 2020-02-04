using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public class DishRepository : IDishRepository
    {
        private readonly DataContext _dataContext;

        public DishRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Dish>> GetAll()
        {
            return await  _dataContext.Dishes.Include(x=>x.DishSupplies).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Dish> GetById(int id)
        {
            return await _dataContext.Dishes.Include(x=>x.DishSupplies).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Dish> GetByName(string name)
        {
            return await _dataContext.Dishes.Include(x => x.DishSupplies).FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<int> GetMaxId()
        {
            return await _dataContext.Dishes.Select(x => x.Id).MaxAsync();
        }

        public void Create(Dish dish)
        {
            _dataContext.Add(dish);
        }

        public void Update(Dish dish)
        {
            _dataContext.Update(dish);
        }

        public void Delete(Dish dish)
        {
            _dataContext.Remove(dish);
        }
    }
}
