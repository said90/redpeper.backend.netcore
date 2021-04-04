using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;

namespace Redpeper.Services.Sales
{
    public interface ISalesService
    {
        Task<List<OrderReportDto>> GetSalesByDate(DateTime date);
        Task<List<OrderReportDto>> GetSalesByRangeDate(DateTime initDate, DateTime endDate);
        Task<Byte[]> SalesExcelByDate(DateTime date);
        Task<Byte[]> SalesExcelByRangeDate(DateTime initDate, DateTime endDate);
    }
}
