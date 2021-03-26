using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Redpeper.Dto;
using Redpeper.Model;

namespace Redpeper.Repositories.Inventory
{
    public interface IInventorySupplyTransactionRepository : IRepository<InventorySupplyTransaction>
    {
        Task<List<InventoryTransactionDto>> ByDate(DateTime date);
        Task<List<InventoryTransactionDto>> ByDateRange(DateTime initDate, DateTime endDate);

        Task<InventoryTransactionDetails> BySupplyIdAndDate(DateTime date, int supplyId);
        Task<InventoryTransactionDetails> BySupplyIdAndDateRange(int supplyId,DateTime initDate, DateTime endDate);
    }
}
