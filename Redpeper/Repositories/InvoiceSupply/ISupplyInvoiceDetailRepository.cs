using System.Collections.Generic;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public interface ISupplyInvoiceDetailRepository
    {
        Task<List<SupplyInvoiceDetail>> GetBySupplyInvoice(int id);
        void Create(SupplyInvoiceDetail detail);
        void CreateRange(List<SupplyInvoiceDetail> details);
        void Update(SupplyInvoiceDetail detail);
        void UpdateRange(List<SupplyInvoiceDetail> details);
        void Remove(SupplyInvoiceDetail detail);
        void RemoveRange(SupplyInvoiceDetail details);

    }
}
