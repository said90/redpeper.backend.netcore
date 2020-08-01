using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<List<Model.Order>> GetAll();
        Task<Model.Order> GetById(int id);
        Task<Model.Order> GetByIdNoTracking(int id);
        Task<List<Model.Order>> GetByRangeId(List<int> ids);
        Task<Model.Order> GetByOrderNumber(string number);
        Task<int> GetOrderNumber();
        Task<List<Model.Order>> GetOrdersByStatus(string status);
        void Create(Model.Order order);
        void Update(Model.Order order);
        void UpdateRange(List<Model.Order> orders);
        void Remove(Model.Order order);


    }
}
