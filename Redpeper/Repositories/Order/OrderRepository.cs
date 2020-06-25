using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Repositories.Orders;

namespace Redpeper.Repositories.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Model.Order>> GetAll()
        {
            return await _dataContext.Orders.Include(x=> x.Table).Include(x=>x.Customer).Include(x=>x.OrderDetails).ToListAsync();
        }

        public async Task<Model.Order> GetById(int id)
        {
            return await _dataContext.Orders.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Model.Order> GetByOrderNumber(string number)
        {
            return await _dataContext.Orders.FirstOrDefaultAsync(x => x.OrderNumber == number);
        }

        public async Task<int> GetOrderNumber()
        {
            return await _dataContext.Orders.CountAsync();
        }

        public async Task<List<Model.Order>> GetOrdersByStatus(string status)
        {
            return await _dataContext.Orders.Where(x => x.Status == status).ToListAsync();
        }

        public void Create(Model.Order order)
        {
            _dataContext.Orders.Add(order);
        }

        public void Update(Model.Order order)
        {
            _dataContext.Orders.Update(order);
        }

        public void Remove(Model.Order order)
        {
            _dataContext.Orders.Remove(order);
        }
    }
}
