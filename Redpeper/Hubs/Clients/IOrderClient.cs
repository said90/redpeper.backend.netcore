using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;

namespace Redpeper.Hubs.Clients
{
    public interface IOrderClient
    {
        Task ReceiveMessage(OrderDto message);

    }
}
