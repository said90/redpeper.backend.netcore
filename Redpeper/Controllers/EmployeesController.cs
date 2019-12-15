using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IEmployeeRepository empleadoRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = empleadoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmpleados()
        {
            return await _employeeRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmpleado(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            try
            {
                _employeeRepository.Create(employee);
                await _unitOfWork.Commit();
                return employee;
            }
            catch (Exception e)
            {
                
                return BadRequest(e);
            }
            
        }

        [HttpPut]

        public async Task<IActionResult> Modify(Employee employee)
        {
            try
            {
                _employeeRepository.Update(employee);
                await _unitOfWork.Commit();
                return Ok(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

        }

        [HttpDelete]
        public async Task<ActionResult<Employee>> Remove(int id)
        {
            var employee = await _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Remove(employee);
            await _unitOfWork.Commit();

            return employee;
        }

    }
}
