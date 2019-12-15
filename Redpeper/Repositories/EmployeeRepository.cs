using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext _dataContext;

        public EmployeeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _dataContext.Employees.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> GetByDui(string dui)
        {
            return await _dataContext.Employees.FirstOrDefaultAsync(x => x.Dui == dui);
        }

        public async Task<Employee> GetByFullName(string name, string lastname)
        {
            return await _dataContext.Employees.FirstOrDefaultAsync(x => x.Name == name && x.Lastname == lastname);
        }

        public void Create(Employee empleado)
        {
            _dataContext.Employees.Add(empleado);
        }

        public void Update(Employee empleado)
        {
            _dataContext.Employees.Update(empleado);
        }

        public void Remove(Employee empleado)
        {
            _dataContext.Employees.Remove(empleado); ;
        }
    }
}
