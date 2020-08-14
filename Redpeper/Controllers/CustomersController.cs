using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {

            return await _unitOfWork.CustomerRepository.GetAllOrderById();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdTask(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            try
            {
                await _unitOfWork.CustomerRepository.InsertTask(customer);
                await _unitOfWork.Commit();
                return customer;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Modify(Customer customer)
        {
            try
            {
                 _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.Commit();
                return Ok(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

        }

        [HttpDelete]
        public async Task<ActionResult<Customer>> Remove(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdTask(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _unitOfWork.CustomerRepository.DeleteTask(id);
            await _unitOfWork.Commit();
            return customer;
        }
    }
}
