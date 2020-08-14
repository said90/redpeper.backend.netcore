using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public class SupplyInvoiceDetailRepository :BaseRepository<SupplyInvoiceDetail>, ISupplyInvoiceDetailRepository
    {
        public SupplyInvoiceDetailRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<SupplyInvoiceDetail>> GetBySupplyInvoice(int id)
        {
            return await _entities.Where(x => x.SupplyInvoiceId == id).ToListAsync();
        }

       
      
    }
}
