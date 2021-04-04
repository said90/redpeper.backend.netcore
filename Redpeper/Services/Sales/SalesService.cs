using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Dto;
using Redpeper.Repositories;
using Redpeper.Services.Sales.Templates;

namespace Redpeper.Services.Sales
{
    public class  SalesService : ISalesService
    {
        private IUnitOfWork _unitOfWork;

        public SalesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderReportDto>> GetSalesByDate(DateTime date)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByDateReport(date);
            return orders;
        }

        public async Task<List<OrderReportDto>> GetSalesByRangeDate(DateTime initDate, DateTime endDate)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByDateRangeReport(initDate, endDate);
            return orders;
        }

        public async Task<Byte[]> SalesExcelByDate(DateTime date)
        {
            var transactions = await _unitOfWork.OrderRepository.GetOrdersByDateReport(date);
            var excel = new SalesExcelTemplate();
            var fileContents = excel.GenerateExcelReport(transactions, date);
            return fileContents;
        }

        public async Task<Byte[]> SalesExcelByRangeDate(DateTime initDate, DateTime endDate)
        {
            var transactions = await _unitOfWork.OrderRepository.GetOrdersByDateRangeReport(initDate, endDate);
            var excel = new SalesExcelByDateRangeTemplate();
            var fileContents = excel.GenerateExcelReport(transactions, initDate, endDate);
            return fileContents;
        }
    }
}
