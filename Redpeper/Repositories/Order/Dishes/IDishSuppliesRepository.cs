using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Dishes
{
    public interface IDishSuppliesRepository
    {
        Task<DishSupply> GetById(int id);
        Task<List<DishSupply>> GetByDishId(int dishId);
        Task<List<DishSupply>> GetByDishIdNoTracking(int dishId);

        void Create(DishSupply dishSupply);
        void CreateRange(List<DishSupply> dishSupplies);
        void Update(DishSupply dishSupply);
        void UpdateRange(List<DishSupply> dishSupplies);

        void Delete(DishSupply dishSupply);
        void DeleteRange(List<DishSupply> dishSupplies);
    }
}
