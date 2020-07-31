using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order
{
    public class OrderDetailRepository :IOrderDetailRepository
    {
        private readonly DataContext _dataContext;

        public OrderDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<OrderDetail> GetById(int id)
        {
            return await _dataContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderDetail>> GetByRangeId(List<int> ids)
        {
            return await _dataContext.OrderDetails.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        
        public async Task<List<OrderDetail>> GetByOrderId(int id)
        {
            return await _dataContext.OrderDetails.Where(x => x.OrderId == id).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetListByStatus(string status)
        {
            return await _dataContext.OrderDetails.Where(x => x.Status == status).ToListAsync();
        }

        public void Create(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Add(orderDetail);
        }

        public void CreateRange(List<OrderDetail> orderDetails)
        {
            _dataContext.OrderDetails.AddRange(orderDetails);
        }

        public void Update(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Update(orderDetail);
        }

        public void UpdateRange(List<OrderDetail> orderDetails)
        {
            _dataContext.OrderDetails.UpdateRange(orderDetails);
        }

        public void Remove(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Remove(orderDetail);
        }

        public void RemoveRange(List<OrderDetail> orderDetails)
        {
            _dataContext.OrderDetails.RemoveRange(orderDetails);
        }
    }
}
