using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void Commit();

        void Rollback();
    }
}
