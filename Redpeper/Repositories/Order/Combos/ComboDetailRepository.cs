using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public class ComboDetailRepository : IComboDetailRepository
    {
        private DataContext _dataContext;

        public ComboDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ComboDetail> GetById(int id)
        {
            return await _dataContext.ComboDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ComboDetail>> GetDetailsByCombo(int comboId)
        {
            return await _dataContext.ComboDetails.Where(x => x.ComboId == comboId).OrderBy(x => x.Id).ToListAsync();
        }
        
        public void Create(ComboDetail comboDetail)
        {
            _dataContext.ComboDetails.Add(comboDetail);
        }

        public void CreateRange(List<ComboDetail> comboDetails)
        {
            _dataContext.ComboDetails.AddRange(comboDetails);
        }

        public void Update(ComboDetail comboDetail)
        {
            _dataContext.ComboDetails.Update(comboDetail);
        }

        public void UpdateRange(List<ComboDetail> comboDetails)
        {
            _dataContext.ComboDetails.UpdateRange(comboDetails);
        }

        public void Delete(ComboDetail comboDetail)
        {
            _dataContext.ComboDetails.Remove(comboDetail);
        }

        public void DeleteRange(List<ComboDetail> comboDetails)
        {
            _dataContext.ComboDetails.RemoveRange(comboDetails);
        }
    }
}
