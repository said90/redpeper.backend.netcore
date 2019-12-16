using System.Collections.Generic;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public interface ISupplyInvoiceDetailRepository
    {
        Task<List<SupplyInvoiceDetail>> GetBySupplyInvoice(int id);
        void Create(SupplyInvoiceDetail detail);
        void Update(SupplyInvoiceDetail detail);
        void Remove(SupplyInvoiceDetail detail);

    }
}
