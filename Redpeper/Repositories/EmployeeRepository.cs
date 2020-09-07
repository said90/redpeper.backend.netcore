using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<List<Employee>> GetAllOrderById()
        {
            return await _entities.OrderBy(x => x.Id).Include(x=>x .User).ToListAsync();
        }

        public async Task<Employee> GetByDui(string dui)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Dui == dui);
        }

        public async Task<Employee> GetByFullName(string name, string lastname)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Name == name && x.Lastname == lastname);
        }
        
       
    }
}
