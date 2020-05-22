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
    public class DishCategoryRepository : IDishCategoryRepository
    {
        private readonly DataContext _dataContext;

        public DishCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<DishCategory>> GetAll()
        {
            return await _dataContext.DishCategories.OrderBy(x=> x.Id).ToListAsync();
        }

        public async Task<PagedList<DishCategory>> GetPaginated(int pageNumber, int pageSize, string sort)
        {
            return await _dataContext.DishCategories.ToPagedListAsync(pageNumber, pageSize, sort);
        }

        public async Task<DishCategory> GetById(int id)
        {
            return await _dataContext.DishCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DishCategory> GetByName(string name)
        {
            return await _dataContext.DishCategories.FirstOrDefaultAsync(x => x.Name == name);
        }

        public void Create(DishCategory dishCategory)
        {
            _dataContext.DishCategories.Add(dishCategory);
        }

        public void Update(DishCategory dishCategory)
        {
            _dataContext.DishCategories.Update(dishCategory);
        }

        public void Remove(DishCategory dishCategory)
        {
            _dataContext.DishCategories.Remove(dishCategory);
        }
    }
}
