using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishRepository : IRepository<Dish>
    {
        Task<List<Dish>> GetAllIncludingSuppliesTask();
        Task<Dish> GetByIdIncludeSuppliesTask(int id);
        Task<Dish> GetByName(string name);
    }
}
