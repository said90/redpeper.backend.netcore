using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public class ComboRepository : BaseRepository<Combo>, IComboRepository
    {

        public ComboRepository(DataContext dataContext) : base(dataContext)
        {
        }
        public async Task<List<Combo>> GetAllInludeDetails()
        {
            return await _entities.Include(x=>x.ComboDetails).OrderBy(x => x.Id).ToListAsync();
        }
        
        public async Task<Combo> GetByName(string name)
        {
            return await _entities.Include(x => x.ComboDetails).FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<int> GetMaxCombo()
        {
            return await _entities.Select(x => x.Id).MaxAsync();
        }
        
    }
}
