using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyRepository _supplyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplyController(ISupplyRepository supplyRepository, IUnitOfWork unitOfWork)
        {
            _supplyRepository = supplyRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supply>>> GetAll()
        {

            return await _supplyRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supply>> Get(int id)
        {
            var supply = await _supplyRepository.GetById(id);

            if (supply == null)
            {
                return NotFound();
            }

            return supply;
        }


        [HttpPost]
        public async Task<ActionResult<Supply>> Create(Supply supply)
        {
            try
            {
                _supplyRepository.Create(supply);
                await _unitOfWork.Commit();
                return supply;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        [HttpPut]
        public async Task<ActionResult<Supply>> Update(Supply supply)
        {
            try
            {
                _supplyRepository.Update(supply);
                await _unitOfWork.Commit();
                return Ok(supply);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

        }

        [HttpDelete]
        public async Task<ActionResult<Supply>> Remove(int id)
        {
            var customer = await _supplyRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _supplyRepository.Remove(customer);
            await _unitOfWork.Commit();
            return customer;
        }
    }
}
