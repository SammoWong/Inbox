using System;
using System.Linq.Expressions;

namespace Inbox.Core.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> expr = null)
            => expr ?? (x => true);

        public static Expression<Func<T1, T2, bool>> Create<T1, T2>(Expression<Func<T1, T2, bool>> expr = null)
            => expr ?? ((x, y) => true);

        public static Expression<Func<T1, T2, T3, bool>> Create<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> expr = null)
            => expr ?? ((x, y, z) => true);
    }
}
