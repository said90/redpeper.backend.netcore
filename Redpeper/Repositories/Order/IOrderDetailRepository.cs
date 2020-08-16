using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetByRangeId(List<int> ids);
        Task<List<OrderDetail>> GetByOrderId(int id);
        Task<List<OrderDetail>> GetListByStatus(string status);
        void RemoveRange(List<OrderDetail> orderDetails);

    }
}
