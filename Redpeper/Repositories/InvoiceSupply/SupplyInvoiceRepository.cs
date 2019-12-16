using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public class SupplyInvoiceRepository :ISupplyInvoiceRepository
    {
        private readonly DataContext _dataContext;

        public SupplyInvoiceRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<SupplyInvoice>> GetAll()
        {
            return await _dataContext.SupplyInvoices.OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<SupplyInvoice> GetById(int id)
        {
            return await _dataContext.SupplyInvoices.FirstOrDefaultAsync(x=> x.Id ==id);
        }

        public async Task<SupplyInvoice> GetByInvoiceNumber(string number)
        {
            return await _dataContext.SupplyInvoices.FirstOrDefaultAsync(x => x.InvoiceNumber == number);
        }

        public async Task<List<SupplyInvoice>> GetByProvider(int id)
        {
            return await _dataContext.SupplyInvoices.Where(x => x.ProviderId == id).ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDate(DateTime date)
        {
            return await _dataContext.SupplyInvoices.Where(x => x.EmissionDate >= date && x.EmissionDate <= date)
                .ToListAsync();
        }

        public async Task<List<SupplyInvoice>> GetByDateRange(DateTime initialDate, DateTime endDate)
        {
            return await _dataContext.SupplyInvoices.Where(x => x.EmissionDate >= initialDate && x.EmissionDate <= endDate)
                .ToListAsync();
        }

        public void Create(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Add(supplyInvoice);
        }

        public void Update(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Update(supplyInvoice);
        }

        public void Remove(SupplyInvoice supplyInvoice)
        {
            _dataContext.SupplyInvoices.Remove(supplyInvoice);
        }
    }
}
