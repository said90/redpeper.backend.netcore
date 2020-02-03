﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
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
            return await _dataContext.DishCategories.ToListAsync();
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
