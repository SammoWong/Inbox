using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Caching.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CachingAttribute : Attribute
    {
        public CachingAttribute(string cacheKey)
        {
            CacheKey = cacheKey;
        }

        /// <summary>
        /// 缓存特性
        /// </summary>
        /// <param name="cacheKey">缓存Key</param>
        /// <param name="seconds">过期时间秒</param>
        public CachingAttribute(string cacheKey, int seconds)
        {
            CacheKey = cacheKey;
            Expiration = TimeSpan.FromSeconds(seconds);
        }

        public string CacheKey { get; set; }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(1);
    }
}
