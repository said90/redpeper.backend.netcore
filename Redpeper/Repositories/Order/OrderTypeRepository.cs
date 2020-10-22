using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order
{
    public class OrderTypeRepository : BaseRepository<OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
