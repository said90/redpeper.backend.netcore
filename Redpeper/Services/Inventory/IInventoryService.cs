﻿using Redpeper.Model;
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
        Task<Byte[]> ActualInventorySupplyExcel();
        Task<Byte[]> ActualInventorySupplyExcelByDateRange(DateTime startDate, DateTime endDate);
        Task<Byte[]> InventoryTransactionsExcel(DateTime date);
        Task<Byte[]> InventoryTransactionsByDateRangeExcel(DateTime initDate, DateTime endDate);

    }
}