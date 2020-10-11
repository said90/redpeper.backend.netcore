using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public class ComboImageRepository : BaseRepository<ComboImage>, IComboImageRepository
    {
        public ComboImageRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<ComboImage> GetByComboId(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.ComboId == id);
        }
    }
}
