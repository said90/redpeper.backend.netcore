using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repositories;

namespace Redpeper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmpleados()
        {
            try
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAllOrderById();
                return Ok(employees);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmpleado(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdTask(id);

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
                await _unitOfWork.EmployeeRepository.InsertTask(employee);
                var user = await _unitOfWork.UserRepository.GetByIdStringTask(employee.UserId);
                if (user!=null)
                {
                    user.Employee = employee;
                    _unitOfWork.UserRepository.Update(user);
                }
                await _unitOfWork.Commit();
                return employee;
            }
            catch (Exception e)
            {

                return BadRequest(new { errors = "This User is Already assign to another Employee",employee  });
            }

        }

        [HttpPut]

        public async Task<IActionResult> Modify(Employee employee)
        {
            try
            {
                _unitOfWork.EmployeeRepository.Update(employee);
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
            var employee = await _unitOfWork.EmployeeRepository.GetByIdTask(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _unitOfWork.EmployeeRepository.DeleteTask(id);
            await _unitOfWork.Commit();

            return employee;
        }

    }
}
