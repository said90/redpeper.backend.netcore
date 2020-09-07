using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order
{
    public class OrderDetailRepository :BaseRepository<OrderDetail>, IOrderDetailRepository
    {

        public OrderDetailRepository(DataContext dataContext) : base(dataContext)
        {
        }

       

        public async Task<List<OrderDetail>> GetByRangeId(List<int> ids)
        {
            return await _entities.Where(x => ids.Contains(x.Id)).Include(x=> x.Combo).Include(x=> x.Dish).ThenInclude(x=> x.DishSupplies).ToListAsync();
        }
        
        public async Task<List<OrderDetail>> GetByOrderId(int id)
        {
            return await _entities.Where(x => x.OrderId == id).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetListByStatus(string status)
        {
            return await _entities.Where(x => x.Status == status).ToListAsync();
        }

 
        public void RemoveRange(List<OrderDetail> orderDetails)
        {
            _entities.RemoveRange(orderDetails);
        }

      
    }
}
