using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public class ComboRepository : IComboRepository
    {
        private readonly DataContext _dataContext;

        public ComboRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Combo>> GetAll()
        {
            return await _dataContext.Combos.Include(x=>x.ComboDetails).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<Combo> GetById(int id)
        {
            return await _dataContext.Combos.Include(x=>x.ComboDetails).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Combo> GetByName(string name)
        {
            return await _dataContext.Combos.Include(x => x.ComboDetails).FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<int> GetMaxCombo()
        {
            return await _dataContext.Combos.Select(x => x.Id).MaxAsync();
        }

        public void Create(Combo combo)
        {
            _dataContext.Combos.Add(combo);
        }

        public void Update(Combo combo)
        {
            _dataContext.Combos.Update(combo);
        }

        public void Remove(Combo combo)
        {
            _dataContext.Combos.Remove(combo);
        }
    }
}
