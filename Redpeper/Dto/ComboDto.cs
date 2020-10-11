using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class ComboDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }
        public IFormFile Image { get; set; }
        public string ComboDetails { get; set; }
    }
}
