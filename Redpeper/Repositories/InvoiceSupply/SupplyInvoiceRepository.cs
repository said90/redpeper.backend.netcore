using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public class SupplyInvoiceRepository :BaseRepository<SupplyInvoice>, ISupplyInvoiceRepository
    {
        public SupplyInvoiceRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<SupplyInvoice>> GetAllIncludingDetails()
        {
            return await _entities.Include(x=>x.Details).ToListAsync();
        }

        public async Task<SupplyInvoice> GetByIdIncludingDetails(int id)
        {
            return await _entities.Include(x => x.Details).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async  Task<int> GetMaxInvoice()
        {
            return await _entities.Select(x=>x.Id).MaxAsync();
        }

        public async Task<SupplyInvoice> GetByInvoiceNumber(string number)
        {
            return await _entities.Include(x => x.Details).FirstOrDefaultAsync(x => x.InvoiceNumber == number);
        }

        public async Task<List<SupplyInvoice>> GetByProvider(int id)
        {
            return await _entities.Include(x => x.Details).Where(x => x.ProviderId == id).ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDate(DateTime date)
        {
            return await _entities.Include(x => x.Details).Where(x => x.EmissionDate >= date && x.EmissionDate <= date)
                .ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDateRange(DateTime initialDate, DateTime endDate)
        {
            return await _entities.Include(x => x.Details).Where(x => x.EmissionDate >= initialDate && x.EmissionDate <= endDate)
                .ToListAsync();
        }
        
    }
}
