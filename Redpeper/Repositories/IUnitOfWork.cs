using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IProviderRepository ProviderRepository { get; }
        ISupplyRepository SupplyRepository { get; }
        ISupplyInvoiceDetailRepository SupplyInvoiceDetailRepository { get; }
        ISupplyInvoiceRepository SupplyInvoiceRepository { get; }
        ITableRepository TableRepository { get; }
        IComboRepository ComboRepository { get; }
        IComboDetailRepository ComboDetailRepository { get; }
        IDishSuppliesRepository DishSuppliesRepository { get; }
        IDishRepository DishRepository { get; }
        IDishCategoryRepository DishCategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        ICurrentInventorySupplyRepository CurrentInventorySupplyRepository { get; }
        IInventorySupplyTransactionRepository InventorySupplyTransactionRepository { get; }
        IUserRepository UserRepository { get; }
        Task Commit();
    }   
}
