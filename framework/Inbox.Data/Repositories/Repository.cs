using Inbox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inbox.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ICommandRepository<TEntity> _commandRepository;
        private readonly IQueryRepository<TEntity> _queryRepository;

        public Repository(ICommandRepository<TEntity> commandRepository, IQueryRepository<TEntity> queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.Any(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _queryRepository.AnyAsync(predicate);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.Count(predicate);
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _queryRepository.CountAsync(predicate);
        }

        public virtual void Delete(object id)
        {
            _commandRepository.Delete(id);
        }

        public virtual void Delete(TEntity entity)
        {
            _commandRepository.Delete(entity);
        }

        public virtual void Delete(params TEntity[] entities)
        {
            _commandRepository.Delete(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            _commandRepository.Delete(entities);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _commandRepository.Delete(predicate);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await _commandRepository.DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _commandRepository.DeleteAsync(predicate);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _queryRepository.FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _queryRepository.FromSql(sql, parameters);
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.Get(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _queryRepository.GetAsync(predicate);
        }

        public virtual void Insert(TEntity entity)
        {
            _commandRepository.Insert(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _commandRepository.InsertAsync(entity);
        }

        public virtual void InsertRange(params TEntity[] entities)
        {
            _commandRepository.InsertRange(entities);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _commandRepository.InsertRange(entities);
        }

        public virtual async Task InsertRangeAsync(params TEntity[] entities)
        {
            await _commandRepository.InsertRangeAsync(entities);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _commandRepository.InsertRangeAsync(entities);
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.LongCount(predicate);
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _queryRepository.LongCountAsync(predicate);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _queryRepository.Query(predicate);
        }

        public virtual void Update(TEntity entity)
        {
            _commandRepository.Update(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await _commandRepository.UpdateAsync(entity);
        }

        public virtual void UpdateRange(params TEntity[] entities)
        {
            _commandRepository.UpdateRange(entities);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _commandRepository.UpdateRange(entities);
        }

        public virtual async Task UpdateRangeAsync(params TEntity[] entities)
        {
            await _commandRepository.UpdateRangeAsync(entities);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _commandRepository.UpdateRangeAsync(entities);
        }
    }
}
