using EasyCaching.Core;
using Inbox.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Caching
{
    public class CacheManager : ICache
    {
        private readonly IEasyCachingProvider _provider;

        public CacheManager(IEasyCachingProvider provider)
        {
            _provider = provider;
        }

        public void Clear()
        {
            _provider.Flush();
        }

        public bool Exists(string cacheKey)
        {
            return _provider.Exists(cacheKey);
        }

        public T Get<T>(string cacheKey)
        {
            return _provider.Get<T>(cacheKey).Value;
        }

        public void Remove(string cacheKey)
        {
            _provider.Remove(cacheKey);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="expiration">为空默认1小时</param>
        public void Set<T>(string cacheKey, T value, TimeSpan? expiration = null)
        {
            expiration = expiration ?? TimeSpan.FromDays(1);
            _provider.Set(cacheKey, value, expiration.GetValueOrDefault());
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="value"></param>
        /// <param name="expiration">为空默认1小时</param>
        /// <returns></returns>
        public bool TrySet<T>(string cacheKey, T value, TimeSpan? expiration = null)
        {
            expiration = expiration ?? TimeSpan.FromDays(1);
            return _provider.TrySet(cacheKey, value, expiration.GetValueOrDefault());
        }
    }
}
