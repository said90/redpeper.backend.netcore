﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Services.Inventory;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IInventoryService _inventoryService;

        public InventoryController(IUnitOfWork unitOfWork, IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryDto>>> GetActualInventory()
        {
            return await _unitOfWork.CurrentInventorySupplyRepository.GetAllActualInventory();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<InventoryDto>>> ByDate(DateTime initDate, DateTime endDate)
        {
            return await _unitOfWork.CurrentInventorySupplyRepository.GetByDateRange(initDate, endDate);
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<List<InventoryDto>>> ByDateExcel(DateTime initDate, DateTime endDate)
        {
            var file = await _inventoryService.ActualInventorySupplyExcelByDateRange(initDate, endDate);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Inventario de Insumos{initDate.Date.ToString("dd/MM/yyyy")}_{endDate.Date.ToString("dd/MM/yyyy")}.xlsx");
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> AllData()
        {
            var file = await _inventoryService.ActualInventorySupplyExcel();
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Inventario de Insumos_{DateTime.Today.Date.ToString("dd/MM/yyyy")}.xlsx");
        }

        [HttpGet("transaction")]
        public async Task<List<InventoryTransactionDto>> Transaction(DateTime date)
        {
            return await _unitOfWork.InventorySupplyTransactionRepository.ByDate(date);
        }

        [HttpGet("transaction/Excel")]
        public async Task<IActionResult> TransactionsExcel(DateTime date)
        {
            var file = await _inventoryService.InventoryTransactionsExcel(date);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle de operaciones_{date.Date.ToString("dd/MM/yyyy")}.xlsx");
        }

        [HttpGet("transaction/DateRange")]
        public async Task<List<InventoryTransactionDto>> TransactionDateRange(DateTime initDate, DateTime endDate)
        {
            return await _unitOfWork.InventorySupplyTransactionRepository.ByDateRange(initDate,endDate);
        }

        [HttpGet("transaction/DateRange/excel")]
        public async Task<IActionResult> TransactionDateRangeExcel(DateTime startDate, DateTime endDate)
        {
            var file = await _inventoryService.InventoryTransactionsByDateRangeExcel(startDate, endDate);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Detalle de operaciones_{startDate.Date:dd/MM/yyyy}_{endDate.Date:dd/MM/yyyy}.xlsx");
        }

        [HttpGet("{supplyId}/transaction/{date}")]
        public async Task<InventoryTransactionDetails> TransactionDetailBySupplyAndDate(int supplyId,DateTime date) 
        {
            return await _unitOfWork.InventorySupplyTransactionRepository.BySupplyIdAndDate(date, supplyId);
        }

        [HttpGet("{supplyId}/transaction/dateRange")]
        public async Task<InventoryTransactionDetails> TransactionDetailBySupplyAndDate(int supplyId, DateTime initDate, DateTime endDate)
        {
            return await _unitOfWork.InventorySupplyTransactionRepository.BySupplyIdAndDateRange(supplyId, initDate, endDate);
        }

        [HttpPost]
        public async Task<IActionResult> InventoryAdjustment(InventoryAdjustmentDto inventoryAdjustment)
        {
            if (!ModelState.IsValid)
                return BadRequest(inventoryAdjustment);

            var inventoryTransaction = new InventorySupplyTransaction
            {
                Date = DateTime.Now,
                SupplyId = inventoryAdjustment.SupplyId,
                Qty = inventoryAdjustment.Qty,
                TransactionType = inventoryAdjustment.Qty>=0?0:1,
                TransactionNumber = "Ajuste de Inventario",
                Comments = inventoryAdjustment.Comments
            };

            await _unitOfWork.InventorySupplyTransactionRepository.InsertTask(inventoryTransaction);
            await _unitOfWork.Commit();
            return Ok(inventoryTransaction);
        }
    }
}
