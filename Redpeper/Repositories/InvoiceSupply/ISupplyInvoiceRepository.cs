using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public interface ISupplyInvoiceRepository
    {
        Task<List<SupplyInvoice>> GetAll();
        Task<SupplyInvoice> GetById(int id);
        Task<int> GetMaxInvoice();
        Task<SupplyInvoice> GetByInvoiceNumber(string number);
        Task<List<SupplyInvoice>> GetByProvider(int id);
        Task<List<SupplyInvoice>> GetByDate(DateTime date);
        Task<List<SupplyInvoice>> GetByDateRange(DateTime initialDate, DateTime endDate);
        void Create(SupplyInvoice supplyInvoice);
        void Update(SupplyInvoice supplyInvoice);
        void Remove(SupplyInvoice supplyInvoice);

    }
}
