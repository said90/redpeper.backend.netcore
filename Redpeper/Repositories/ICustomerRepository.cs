using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetById(int id);
        Task<Customer> GetByDui(string dui);
        Task<Customer> GetByFullName(string name, string lastname);
        void Create(Customer cliente);
        void Update(Customer cliente);
        void Remove(Customer cliente);
    }
}
