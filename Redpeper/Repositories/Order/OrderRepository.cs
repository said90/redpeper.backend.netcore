using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories.Orders;

namespace Redpeper.Repositories.Order
{
    public class OrderRepository :BaseRepository<Model.Order>, IOrderRepository
    {
        public OrderRepository(DataContext dataContext) : base(dataContext)
        {
        }


        public async Task<List<Model.Order>> GetAllWithIncludes()
        {
            return await _entities.Include(x=> x.Table).Include(x=>x.Customer).Include(x=>x.OrderDetails).ToListAsync();
        }

        public async Task<List<Model.Order>> GetActiveOrders()
        {
            return await _entities.Include(x => x.OrderDetails).Where(x => !x.Status.Equals("Cobrado") && !x.Status.Equals("Anulado")).ToListAsync();
        }

        public async Task<List<Model.Order>> GetOrderByEmployee(int employeeId)
        {
            return await _entities.Include(x => x.Table).Include(x => x.Customer).Include(x => x.OrderDetails).Where(x=>x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<Model.Order> GetByIdWithDetails(int id)
        {
            return await _entities.Include(x=>x.OrderDetails).FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Model.Order> GetByIdNoTracking(int id)
        {
            return await _entities.Include(x => x.OrderDetails).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<Model.Order>> GetByRangeId(List<int> ids)
        {
            return await _entities.Include(x=>x.OrderDetails).Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Model.Order>> GetByRangeIdNoIncludes(List<int> ids)
        {
            return await _entities.Where(x => ids.Contains(x.Id)).Distinct().ToListAsync();
        }

        public async Task<Model.Order> GetByOrderNumber(string number)
        {
            return await _entities.FirstOrDefaultAsync(x => x.OrderNumber == number);
        }

        public async Task<string> GetOrderNumber(int orderId)
        {
            return await _entities.Where(x=> x.Id == orderId).Select(x=> x.OrderNumber).SingleOrDefaultAsync();
        }

        public async Task<List<Model.Order>> GetOrdersByStatus(string status)
        {
            return await _entities.Where(x => x.Status == status).ToListAsync();
        }

    }
}
