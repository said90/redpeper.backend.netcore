using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<Employee> GetByDui(string dui);
        Task<Employee> GetByFullName(string name, string lastname);
        void Create(Employee empleado);
        void Update(Employee empleado);
        void Remove(Employee empleado);
    }
}
