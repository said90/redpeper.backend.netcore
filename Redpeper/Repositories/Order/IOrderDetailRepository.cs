using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> GetById(int id);
        Task<List<OrderDetail>> GetByRangeId(List<int> ids);
        Task<List<OrderDetail>> GetByOrderId(int id);
        Task<List<OrderDetail>> GetListByStatus(string status);
        void Create(OrderDetail orderDetail);
        void CreateRange(List<OrderDetail> orderDetails);
        void Update(OrderDetail orderDetail);
        void UpdateRange(List<OrderDetail> orderDetails);
        void Remove(OrderDetail orderDetail);
        void RemoveRange(List<OrderDetail> orderDetails);

    }
}
