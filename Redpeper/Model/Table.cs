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
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public double Chairs { get; set; }
        public int State { get; set; }
        public virtual User User { get; set; }
    }
}
