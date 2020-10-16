using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class NotificationOrderDto
    {
        public string Token { get; set; }
        public string Title { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
