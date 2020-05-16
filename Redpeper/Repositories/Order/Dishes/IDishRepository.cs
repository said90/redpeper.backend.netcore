﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishRepository
    {
        Task<List<Dish>> GetAll();
        Task<Dish> GetById(int id);
        Task<Dish> GetByName(string name);
        Task<int> GetMaxId();
        void Create(Dish dish);
        void Update(Dish dish);
        void Delete(Dish dish);
    }
}