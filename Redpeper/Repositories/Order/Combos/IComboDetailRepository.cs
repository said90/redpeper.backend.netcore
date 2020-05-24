using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories.Order.Combos
{
    public interface IComboDetailRepository
    {
        Task<ComboDetail> GetById(int id);
        Task<List<ComboDetail>> GetDetailsByCombo(int comboId);
        Task<List<ComboDetail>> GetDetailsByComboNoTracking(int comboId);

        void Create(ComboDetail comboDetail);
        void CreateRange(List<ComboDetail> comboDetails);
        void Update(ComboDetail comboDetail);
        void UpdateRange(List<ComboDetail> comboDetails);
        void Delete(ComboDetail comboDetail);
        void DeleteRange(List<ComboDetail> comboDetails);

    }
}
