using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private DataContext _dataContext;

        public CustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _dataContext.Customers.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> GetByDui(string dui)
        {
            return await _dataContext.Customers.FirstOrDefaultAsync(x => x.Dui == dui);
        }

        public async Task<Customer> GetByFullName(string name, string lastname)
        {
            return await _dataContext.Customers.FirstOrDefaultAsync(x => x.Name == name && x.Lastname == lastname);
        }

        public void Create(Customer cliente)
        {
            _dataContext.Customers.Add(cliente);
        }

        public void Update(Customer cliente)
        {
            _dataContext.Customers.Update(cliente);
        }

        public void Remove(Customer cliente)
        {
            _dataContext.Customers.Remove(cliente); ;
        }
    }
}
