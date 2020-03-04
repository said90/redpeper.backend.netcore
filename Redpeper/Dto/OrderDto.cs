using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public String Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
