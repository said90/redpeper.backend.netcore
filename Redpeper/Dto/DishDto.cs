using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class DishDto
    {
        public int Id { get; set; }
        public int DishCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
        public string DishSupplies { get; set; }
    }
}
