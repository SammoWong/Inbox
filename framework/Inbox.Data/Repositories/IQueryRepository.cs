using Inbox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inbox.Data.Repositories
{
    public interface IQueryRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate = null);

        int Count(Expression<Func<TEntity, bool>> predicate = null);
        
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        long LongCount(Expression<Func<TEntity, bool>> predicate = null);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null);

        bool Any(Expression<Func<TEntity, bool>> predicate = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}
