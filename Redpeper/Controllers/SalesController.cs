using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Redpeper.Services.Sales;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ByDate(DateTime date)
        {
            var order = await _salesService.GetSalesByDate(date);
            return Ok(order);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ByDateRange(DateTime initDate, DateTime endDate)
        {
            var order = await _salesService.GetSalesByRangeDate(initDate,endDate);
            return Ok(order);
        }

        [HttpGet("[action]/excel")]
        public async Task<IActionResult> Date(DateTime date)
        {
            var file = await _salesService.SalesExcelByDate(date);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle de Ventas_{date.Date.ToString("dd/MM/yyyy")}.xlsx");
        }

        [HttpGet("[action]/excel")]
        public async Task<IActionResult> DateRange(DateTime initDate, DateTime endDate)
        {
            var file = await _salesService.SalesExcelByRangeDate(initDate,endDate);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle de Ventas_{initDate.Date:dd/MM/yyyy}_{endDate.Date:dd/MM/yyyy}.xlsx");
        }
    }
}
