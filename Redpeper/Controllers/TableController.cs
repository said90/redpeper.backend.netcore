using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Redpeper.Collection;
using Redpeper.Model;
using Redpeper.Repositories;
using Redpeper.Repositories.Tables;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public TableController(ITableRepository tableRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _tableRepository = tableRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Table>>> GetAll()
        {
            return await _tableRepository.GetAll();
        }

        // [HttpGet]
        // public async Task<PagedList<Table>> GetPaginated(int page = 1, int size = 10, string sort = "")
        // {
        //     var result = await _tableRepository.GetPaginated(page, size, sort);
        //     var metadata = new
        //     {
        //         result.TotalCount,
        //         result.ItemPerPage,
        //         result.Page,
        //         result.TotalPages,
        //         result.HasMorePages,
        //         result.HasPrevPages,
        //         result.Sort
        //     };
        //     Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        //     return result;
        // }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Table>> GetById(int id)
        {
            return await _tableRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Table>> Create([FromBody] Table table)
        {
            try
            {
                _tableRepository.Create(table);
                await _unitOfWork.Commit();
                return table;
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }


        [HttpPut]
        public async Task<ActionResult<Table>> Update([FromBody] Table table)
        {
            try
            {

                if (table.CustomerId == null)
                {
                    var customer = new Customer
                    {
                        Name = table.CustomerName,
                        Lastname = table.CustomerLastName
                    };
                    _customerRepository.Create(customer);
                    await _unitOfWork.Commit();
                    table.CustomerId = customer.Id;
                }
                _tableRepository.Update(table);
                await _unitOfWork.Commit();
                return Ok(table);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
        [HttpPatch]
        public async Task<ActionResult<Table>> Change([FromBody] Table table)
        {
            try
            {

                if (table.CustomerId == 0)
                {
                    var customer = new Customer
                    {
                        Name = table.CustomerName,
                        Lastname = table.CustomerLastName
                    };
                    _customerRepository.Create(customer);
                    await _unitOfWork.Commit();
                    table.CustomerId = customer.Id;
                }

                _tableRepository.Update(table);
                await _unitOfWork.Commit();
                return Ok(table);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Table>> Remove(int id)
        {
            var table = await _tableRepository.GetById(id);
            if (table == null)
            {
                return NotFound();
            }

            _tableRepository.Remove(table);
            await _unitOfWork.Commit();
            return table;
        }
    }
}
