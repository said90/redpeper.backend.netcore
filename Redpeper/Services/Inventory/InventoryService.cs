using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.PlatformAbstractions.Native;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Inventory;

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
                ExpirationDate = x.ExpirationDate,
                SupplyId = x.SupplyId,
                Date = x.Date,
                Qty = x.Qty
            }).ToList();
            await _unitOfWork.CurrentInventorySupplyRepository.InsertRangeTask(inventoryDetails);
            await _unitOfWork.InventorySupplyTransactionRepository.InsertRangeTask(inventoryTransaction);
            await _unitOfWork.Commit();
        }
    }
}