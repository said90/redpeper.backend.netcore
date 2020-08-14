using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface IEmployeeRepository :IRepository<Employee>
    {
        Task<List<Employee>> GetAllOrderById();
        Task<Employee> GetByDui(string dui);
        Task<Employee> GetByFullName(string name, string lastname);
       
    }
}
