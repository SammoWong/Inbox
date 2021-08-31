using Inbox.Data.Entities;

namespace Inbox.Data.Repositories
{
    public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICommandRepository<TEntity>
        where TEntity : class, IEntity
    {
    }
}
