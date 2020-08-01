using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;
using Redpeper.Model;

namespace Redpeper.Hubs.Clients
{
    public interface IOrderClient
    {
        Task OrderCreated(OrderDto message);
        Task DetailsInProcess(List<OrderDetail> details);
        Task DetailsFinished(List<OrderDetail> details);
        Task DetailsDelivered(List<OrderDetail> details);



    }
}
