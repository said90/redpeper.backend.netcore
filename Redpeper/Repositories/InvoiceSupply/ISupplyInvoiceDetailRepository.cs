using System.Collections.Generic;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.InvoiceSupply
{
    public interface ISupplyInvoiceDetailRepository : IRepository<SupplyInvoiceDetail>
    {
        Task<List<SupplyInvoiceDetail>> GetBySupplyInvoice(int id);
       
    }
}
