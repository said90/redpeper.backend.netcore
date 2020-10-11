using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories.Inventory;
using Redpeper.Repositories.InvoiceSupply;
using Redpeper.Repositories.Order;
using Redpeper.Repositories.Order.Combos;
using Redpeper.Repositories.Order.Dishes;
using Redpeper.Repositories.Orders;
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
            ComboImageRepository = new ComboImageRepository(_dataContext);
            DishSuppliesRepository = new DishSuppliesRepository(_dataContext);
            DishRepository = new DishRepository(_dataContext);
            DishCategoryRepository = new DishCategoryRepository(_dataContext);
            DishImageRepository = new DishImageRepository(_dataContext);
            OrderRepository = new OrderRepository(_dataContext);
            OrderDetailRepository = new OrderDetailRepository(_dataContext);
            CurrentInventorySupplyRepository = new CurrentInventorySupplyRepository(_dataContext);
            InventorySupplyTransactionRepository = new InventorySupplyTransactionRepository(_dataContext);
            UserRepository= new UserRepository(dataContext);
        }

        public IEmployeeRepository EmployeeRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IProviderRepository ProviderRepository { get; set; }
        public ISupplyRepository SupplyRepository { get; set; }
        public ISupplyInvoiceDetailRepository SupplyInvoiceDetailRepository { get; set; }
        public ISupplyInvoiceRepository SupplyInvoiceRepository { get; set; }
        public ITableRepository TableRepository { get; set; }
        public IComboRepository ComboRepository { get; set; }
        public IComboDetailRepository ComboDetailRepository { get; set; }
        public IComboImageRepository ComboImageRepository { get; set; }
        public IDishSuppliesRepository DishSuppliesRepository { get; set; }
        public IDishRepository DishRepository { get; set; }
        public IDishImageRepository DishImageRepository { get; set; }
        public IDishCategoryRepository DishCategoryRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public ICurrentInventorySupplyRepository CurrentInventorySupplyRepository { get; set; }
        public IInventorySupplyTransactionRepository InventorySupplyTransactionRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

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
