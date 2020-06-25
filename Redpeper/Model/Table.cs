using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Redpeper.Model
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public int Chairs { get; set; }
        public int State { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
