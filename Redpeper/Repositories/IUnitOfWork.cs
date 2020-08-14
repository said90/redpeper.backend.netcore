using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;
using Redpeper.Repositories.InvoiceSupply;

namespace Redpeper.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IProviderRepository ProviderRepository { get; }
        ISupplyRepository SupplyRepository { get; }
        ISupplyInvoiceDetailRepository SupplyInvoiceDetailRepository { get; }
        ISupplyInvoiceRepository SupplyInvoiceRepository { get; }
        Task Commit();
    }   
}
