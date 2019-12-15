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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {

            return await _customerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _customerRepository.GetById(id);

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
                _customerRepository.Create(customer);
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
                _customerRepository.Update(customer);
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
            var customer = await _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Remove(customer);
            await _unitOfWork.Commit();
            return customer;
        }
    }
}
