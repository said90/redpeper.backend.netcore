using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Redpeper.Data;
using Redpeper.Dto;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public class InventorySupplyTransactionRepository : BaseRepository<InventorySupplyTransaction>,
        IInventorySupplyTransactionRepository
    {
        public InventorySupplyTransactionRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<InventoryTransactionDto>> ByDate(DateTime date)
        {
            return await _entities
                .Where(x => x.Date.Date == date).Select(y =>
                    new InventoryTransactionDto
                    {
                        TransactionType = y.TransactionType == 0 ? "Compra" : "Venta",
                        TransationNumber = y.TransactionNumber,
                        Date = y.Date,
                        Supply = y.Supply.Name,
                        Qty = y.Qty,
                        Comments = y.Comments
                    })
                .OrderBy(x => x.Date).ToListAsync();

        }

        public async Task<List<InventoryTransactionDto>> ByDateRange(DateTime initDate, DateTime endDate)
        {
            return await _entities
                .Where(x => x.Date >= initDate.Date && x.Date.Date <= endDate).Select(y =>
                    new InventoryTransactionDto
                    {
                        Id = y.Id,
                        TransactionType = y.TransactionType == 0 ? "Compra" : "Venta",
                        TransationNumber = y.TransactionNumber,
                        Date = y.Date,
                        Supply = y.Supply.Name,
                        Qty = y.Qty,
                        Comments = y.Comments
                    })
                .OrderBy(x => x.Date).ToListAsync();
        }

        public async Task<InventoryTransactionDetails> BySupplyIdAndDate(DateTime date, int supplyId)
        {
            var transactionDetails = new InventoryTransactionDetails
            {
                InventoryTransactions = await _entities
                    .Where(x => x.Date == date.Date && x.SupplyId == supplyId).Select(y =>
                        new InventoryTransactionDto
                        {
                            Id = y.Id,
                            TransactionType = y.TransactionType ==0? "Compra": "Venta",
                            TransationNumber = y.TransactionNumber,
                            Date = y.Date,
                            Supply = y.Supply.Name, 
                            Qty = y.Qty,
                            Comments = y.Comments
                        })
                    .OrderBy(x => x.Date).ToListAsync()
            };

            transactionDetails.Total = transactionDetails.InventoryTransactions.Sum(x => x.Qty);
            return transactionDetails;
        }

        public async Task<InventoryTransactionDetails> BySupplyIdAndDateRange(int supplyId, DateTime startDate,
            DateTime enDate)
        {
            var transactionDetails = new InventoryTransactionDetails
            {
                InventoryTransactions = await _entities
                    .Where(x => x.Date >= startDate.Date && x.Date <= enDate.Date && x.SupplyId == supplyId)
                    .Select(y => new InventoryTransactionDto
                    {
                        Id = y.Id,
                        TransactionType = y.TransactionType == 0 ? "Compra" : "Venta",
                        TransationNumber = y.TransactionNumber,
                        Date = y.Date,
                        Supply = y.Supply.Name,
                        Qty = y.Qty,
                        Comments = y.Comments
                    })
                    .OrderBy(x => x.Date).ToListAsync()
            };

            transactionDetails.Total = transactionDetails.InventoryTransactions.Sum(x => x.Qty);
            return transactionDetails;
        }
    }
}