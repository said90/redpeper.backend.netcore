using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderTypes()
        {
            var orderType = await _unitOfWork.OrderTypeRepository.GetAllTask();
            return Ok(orderType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderType(int id)
        {
            var orderType = await _unitOfWork.OrderTypeRepository.GetByIdTask(id);
            if (orderType == null)
            {
                return NotFound(id);

            }

            return Ok(orderType);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderType(OrderType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(type);

            }

            await _unitOfWork.OrderTypeRepository.InsertTask(type);
            await _unitOfWork.Commit();

            return Ok(type);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderType(OrderType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(type);

            }

             _unitOfWork.OrderTypeRepository.Update(type);
             await _unitOfWork.Commit();

            return Ok(type);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderType(int id)
        {
            var orderType = await _unitOfWork.OrderTypeRepository.GetByIdTask(id);

            if (orderType ==null)
            {
                return NotFound(id);
            }
            await _unitOfWork.OrderTypeRepository.DeleteTask(id);
            await _unitOfWork.Commit();
            return Ok(orderType);
        }
    }
}
