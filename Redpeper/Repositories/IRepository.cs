using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Collection;

namespace Redpeper.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllTask();

        Task<PagedList<TEntity>> GetAllPageableTask(int pageNumber, int itemsPerPage, string sortBy);

        Task<TEntity> GetByIdTask(int? id);

        Task<bool> ExistAsync(int id);

        Task<int> CountTask();

        Task<TEntity> LastRegisterTask();

        Task<TEntity> InsertTask(TEntity entity);

        Task<IEnumerable<TEntity>> InsertRangeTask(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task DeleteTask(int id);

        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
