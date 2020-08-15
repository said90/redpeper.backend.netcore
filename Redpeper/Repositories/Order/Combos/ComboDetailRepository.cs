using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public class ComboDetailRepository :BaseRepository<ComboDetail>, IComboDetailRepository
    {
        public ComboDetailRepository(DataContext dataContext) : base(dataContext)
        {
        }
 
        public async Task<List<ComboDetail>> GetDetailsByCombo(int comboId)
        {
            return await _entities.Where(x => x.ComboId == comboId).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<List<ComboDetail>> GetDetailsByComboNoTracking(int comboId)
        {
            return await _entities.Where(x => x.ComboId == comboId).OrderBy(x => x.Id).AsNoTracking().ToListAsync();
        }
        
    }
}
