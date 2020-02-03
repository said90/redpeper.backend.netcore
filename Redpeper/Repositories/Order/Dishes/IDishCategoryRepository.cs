using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishCategoryRepository
    {
        Task<List<DishCategory>> GetAll();
        Task<DishCategory> GetById(int id);
        Task<DishCategory> GetByName(string name);
        void Create(DishCategory dishCategory);
        void Update(DishCategory dishCategory);
        void Remove(DishCategory dishCategory);

    }
}
