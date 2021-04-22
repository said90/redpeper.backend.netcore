using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Dto
{
    public class ChangeOrderDetailDto
    {
        public List<int> DetailsId { get; set; }
        public int Status { get; set; }
        public bool Tip { get; set; }
    }
}
