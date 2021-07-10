using System.Reflection;
using System.Threading.Tasks;

namespace Inbox.Core.Helpers
{
    public static class MethodHelper
    {
        /// <summary>
        /// 判断是否异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }
    }
}
