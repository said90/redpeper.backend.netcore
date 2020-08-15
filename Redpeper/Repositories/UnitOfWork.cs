using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories.InvoiceSupply;
using Redpeper.Repositories.Order.Combos;
using Redpeper.Repositories.Tables;

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
            TableRepository = new TableRepository(_dataContext);
            ComboRepository = new ComboRepository(_dataContext);
            ComboDetailRepository = new ComboDetailRepository(_dataContext);
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }
        public ISupplyRepository SupplyRepository { get; set; }
        public ISupplyInvoiceDetailRepository SupplyInvoiceDetailRepository { get; set; }
        public ISupplyInvoiceRepository SupplyInvoiceRepository { get; set; }
        public ITableRepository TableRepository { get; set; }
        public IComboRepository ComboRepository { get; }
        public IComboDetailRepository ComboDetailRepository { get; }

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
