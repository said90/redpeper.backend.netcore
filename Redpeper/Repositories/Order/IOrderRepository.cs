using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Repositories.Orders
{
    public interface IOrderRepository :IRepository<Model.Order>
    {
        Task<List<Model.Order>> GetAllWithIncludes();
        Task<List<Model.Order>> GetActiveOrders();
        Task<List<Model.Order>> GetOrderByEmployee(int employeeId);
        Task<Model.Order> GetByIdWithDetails(int id);
        Task<Model.Order> GetByIdNoTracking(int id);
        Task<List<Model.Order>> GetByRangeId(List<int> ids);
        Task<Model.Order> GetByOrderNumber(string number);
        Task<int> GetOrderNumber();
        Task<List<Model.Order>> GetOrdersByStatus(string status);
    }
}
