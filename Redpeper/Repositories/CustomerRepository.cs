using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class CustomerRepository :BaseRepository<Customer>,ICustomerRepository
    {
        public CustomerRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Customer>> GetAllOrderById()
        {
            return await _entities.OrderBy(x => x.Id).ToListAsync();
        }


        public async Task<Customer> GetByDui(string dui)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Dui == dui);
        }

        public async Task<Customer> GetByFullName(string name, string lastname)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Name == name && x.Lastname == lastname);
        }

   }
}
