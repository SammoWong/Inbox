using Inbox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inbox.Data.Repositories
{
    public interface ICommandRepository<TEntity> where TEntity : class, IEntity
    {
        //int ExecuteSqlCommand(string sql, params object[] parameters);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void InsertRange(params TEntity[] entities);

        Task InsertRangeAsync(params TEntity[] entities);

        void InsertRange(IEnumerable<TEntity> entities);

        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the entity by primary key.
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(params TEntity[] entities);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void UpdateRange(params TEntity[] entities);

        Task UpdateRangeAsync(params TEntity[] entities);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        //void Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> expression);

        //Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> expression);
    }
}
