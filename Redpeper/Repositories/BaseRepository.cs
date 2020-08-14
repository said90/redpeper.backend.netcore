using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Collection;
using Redpeper.Data;
using Redpeper.Extensions;

namespace Redpeper.Repositories
{
    public class BaseRepository<T>: IRepository<T> where T : class
    {
        private readonly DataContext _dataContext;
        protected readonly DbSet<T> _entities; 



        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _entities = dataContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllTask()
        {
             return await  _entities.ToListAsync();
        }

        //OrderBy(c => c.Id );
        //public async Task<IEnumerable<T>> GetAllOrderByTask<T, TKey>(Expression<Func<T, TKey>> predicate) where T : class
        //{
        //    return await _dataContext.Set<T>().OrderBy(predicate).ToListAsync();
        //}

        public async Task<PagedList<T>> GetAllPageableTask(int pageNumber, int itemsPerPage, string sortBy)
        {
            return await _entities.ToPagedListAsync(pageNumber, itemsPerPage, sortBy);
        }

        public async Task<T> GetByIdTask(int id)
        {
            try
            {
                return await _entities.FindAsync(id);

            }
            catch (Exception e)
            {
                var erro = e.Message;
                return null;
            }
        }


        public async Task<bool> ExistAsync(int id)
        {
            try
            {
                var obj = await _dataContext.Set<T>().FindAsync(id);
                _dataContext.Entry(obj).State = EntityState.Detached;
                return obj != null;
            }
            catch (Exception ex)
            {
                var erro = ex.Message;
                return false;
            }
        }

        public async Task<int> CountTask()
        {
            return await _entities.CountAsync();
        }

        public async Task<T> LastRegisterTask()
        {
            return await _entities.LastAsync();
        }

        public async Task<T> InsertTask(T entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> InsertRangeTask(IEnumerable<T> entities)
        {
            var insertRangeTask = entities.ToList();
            await _entities.AddRangeAsync(insertRangeTask);

            return insertRangeTask;
        }

        public void Update(T entity)
        {
            _entities.Update(entity);

        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public async Task DeleteTask(int id)
        {
            var entity = await GetByIdTask(id);

            if (entity == null)
                throw new Exception("The entity is null");

            _entities.Remove(entity);

        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
