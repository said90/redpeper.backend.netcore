using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories.InvoiceSupply;

namespace Redpeper.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;


        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
            EmployeeRepository = new EmployeeRepository(_dataContext);
            CustomerRepository = new CustomerRepository(_dataContext);
            ProviderRepository = new ProviderRepository(_dataContext);
            SupplyRepository = new SupplyRepository(_dataContext);
            SupplyInvoiceDetailRepository = new SupplyInvoiceDetailRepository(_dataContext);
            SupplyInvoiceRepository =  new SupplyInvoiceRepository(_dataContext);
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }
        public ISupplyRepository SupplyRepository { get; set; }
        public ISupplyInvoiceDetailRepository SupplyInvoiceDetailRepository { get; set; }
        public ISupplyInvoiceRepository SupplyInvoiceRepository { get; set; }

        public async Task Commit()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_dataContext!=null)
            {
                _dataContext.Dispose();
            }

        }
    }
}
