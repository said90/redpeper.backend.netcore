using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public class SupplyInvoiceDetailRepository :ISupplyInvoiceDetailRepository
    {
        private DataContext _dataContext;

        public SupplyInvoiceDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<SupplyInvoiceDetail>> GetBySupplyInvoice(int id)
        {
            return await _dataContext.SupplyInvoiceDetails.Where(x => x.SupplyInvoiceId == id).ToListAsync();
        }

        public void Create(SupplyInvoiceDetail detail)
        {
            _dataContext.SupplyInvoiceDetails.Add(detail);
        }

        public void CreateRange(List<SupplyInvoiceDetail> details)
        {
            _dataContext.SupplyInvoiceDetails.AddRange(details);
        }

        public void Update(SupplyInvoiceDetail detail)
        {
            _dataContext.SupplyInvoiceDetails.Update(detail);
        }
        public void UpdateRange(List<SupplyInvoiceDetail> details)

        {
            _dataContext.SupplyInvoiceDetails.UpdateRange(details);
        }

        public void Remove(SupplyInvoiceDetail detail)
        {
            _dataContext.SupplyInvoiceDetails.Remove(detail);
        }
        public void RemoveRange(SupplyInvoiceDetail details)
        {
            _dataContext.SupplyInvoiceDetails.RemoveRange(details);
        }
    }
}
