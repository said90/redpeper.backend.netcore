﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.PlatformAbstractions.Native;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.Utils;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Inventory;
using Redpeper.Services.Inventory.Templates;

namespace Redpeper.Services.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public InventoryService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task   AddInvoiceDetailToInventory(InvoiceSupplyDto invoice)
        {
            var inventoryDetails = invoice.Details.Select(x => new CurrentInventorySupply
            {
                TransactionType = 0,
                TransactionNumber = invoice.InvoiceNumber,
                ExpirationDate = x.ExpirationDate,
                Qty = x.Quantity,
                SupplyId = x.SupplyId
            }).ToList();

            inventoryDetails.ForEach(x =>
            {
                x.Date = invoice.EmissionDate;
            });
            var inventoryTransaction= inventoryDetails.Select(x => new InventorySupplyTransaction
            {
                TransactionNumber = x.TransactionNumber,
                ExpirationDate = x.ExpirationDate,
                SupplyId = x.SupplyId,
                Date = x.Date,
                Qty = x.Qty
            }).ToList();
            await _unitOfWork.CurrentInventorySupplyRepository.InsertRangeTask(inventoryDetails);
            await _unitOfWork.InventorySupplyTransactionRepository.InsertRangeTask(inventoryTransaction);
            await _unitOfWork.Commit();
        }

        public async Task<Byte[]> ActualInventorySupplyExcel()
        {
            var inventory = await _unitOfWork.CurrentInventorySupplyRepository.GetAllActualInventory();
            var excel = new InventoryExcelTemplate();
            var fileContents = excel.GenerateExcelReport(inventory);
            return fileContents;
        }

        public async Task<Byte[]> ActualInventorySupplyExcelByDateRange(DateTime startDate, DateTime endDate)
        {
            var inventory = await _unitOfWork.CurrentInventorySupplyRepository.GetByDateRange(startDate,endDate);
            var excel = new InventoryExcelTemplate();
            var fileContents = excel.GenerateExcelReport(inventory);
            return fileContents;
        }

        public async Task<Byte[]> InventoryTransactionsExcel(DateTime date)
        {
            var transactions = await _unitOfWork.InventorySupplyTransactionRepository.ByDate(date);
            var excel = new TransactionsInventoryExcelTemplate();
            var fileContents = excel.GenerateExcelReport(transactions, date);
            return fileContents;
        }

        public async Task<Byte[]> InventoryTransactionsByDateRangeExcel(DateTime initDate, DateTime endDate)
        {
            var transactions = await _unitOfWork.InventorySupplyTransactionRepository.ByDateRange(initDate,endDate);
            var excel = new TransactionsInventoryByDateRangeExcelTemplate();
            var fileContents = excel.GenerateExcelReport(transactions, initDate,endDate);
            return fileContents;
        }
    }
}
