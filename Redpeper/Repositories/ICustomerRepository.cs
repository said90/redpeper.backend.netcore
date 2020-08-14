using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetAllOrderById();
        Task<Customer> GetByDui(string dui);
        Task<Customer> GetByFullName(string name, string lastname);
   
    }
}
