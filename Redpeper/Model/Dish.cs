using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpeper.Model
{
    public class Dish
    {
        public int Id { get; set; }
        public int DishCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public virtual List<DishSupply> DishSupplies { get; set; }
    }
}
