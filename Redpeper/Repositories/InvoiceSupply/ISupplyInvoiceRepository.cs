using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redpeper.Collection;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public interface ISupplyInvoiceRepository :IRepository<SupplyInvoice>
    {
        Task<List<SupplyInvoice>> GetAllIncludingDetails();
        Task<SupplyInvoice> GetByIdIncludingDetails(int id);
        Task<int> GetMaxInvoice();
        Task<SupplyInvoice> GetByInvoiceNumber(string number);
        Task<List<SupplyInvoice>> GetByProvider(int id);
        Task<List<SupplyInvoice>> GetByDate(DateTime date);
        Task<List<SupplyInvoice>> GetByDateRange(DateTime initialDate, DateTime endDate);
    }
}
