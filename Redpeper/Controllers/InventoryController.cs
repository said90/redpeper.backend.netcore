using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpeper.Dto;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Services.Inventory;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult<List<InventoryDto>>> GetAll()
        {
            return await _unitOfWork.CurrentInventorySupplyRepository.GetAllActualInventory();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Download()
        {
            var file = await _inventoryService.ActualInventorySupplyExcel();
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Inventario Actual de insumos.xlsx");
        }
    }
}
