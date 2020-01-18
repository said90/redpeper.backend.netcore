using Redpeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;

namespace Redpeper.Services.Inventory
{
    public interface IInventoryService
    {
        Task AddInvoiceDetailToInventory(InvoiceSupplyDto invoice);
    }
}