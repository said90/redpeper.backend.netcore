using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;

namespace Redpeper.Model
{
    public class OrderType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
