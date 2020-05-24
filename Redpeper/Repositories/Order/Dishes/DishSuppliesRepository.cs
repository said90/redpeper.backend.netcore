using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public class DishSuppliesRepository : IDishSuppliesRepository
    {
        private readonly DataContext _dataContext;

        public DishSuppliesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<DishSupply> GetById(int id)
        {
            return await  _dataContext.DishSupplies.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<DishSupply>> GetByDishId(int dishId)
        {
            return await _dataContext.DishSupplies.Where(x => x.DishId == dishId).ToListAsync();
        }

        public async Task<List<DishSupply>> GetByDishIdNoTracking(int dishId)
        {
            return await _dataContext.DishSupplies.Where(x => x.DishId == dishId).AsNoTracking().ToListAsync();
        }

        public void Create(DishSupply dishSupply)
        {
            _dataContext.DishSupplies.Add(dishSupply);
        }

        public void CreateRange(List<DishSupply> dishSupplies)
        {
            _dataContext.DishSupplies.AddRange(dishSupplies);
        }

        public void Update(DishSupply dishSupply)
        {
            _dataContext.DishSupplies.Update(dishSupply);
        }

        public void UpdateRange(List<DishSupply> dishSupplies)
        {
            _dataContext.DishSupplies.UpdateRange(dishSupplies);
        }

        public void Delete(DishSupply dishSupply)
        {
            _dataContext.DishSupplies.Remove(dishSupply);
        }
        public void DeleteRange(List<DishSupply> dishSupplies)
        {
            _dataContext.DishSupplies.RemoveRange(dishSupplies);
        }
    }
}

