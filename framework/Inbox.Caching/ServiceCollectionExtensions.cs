using EasyCaching.Core.Configurations;
using Inbox.Core.Caching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Inbox.Caching
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCaching(this IServiceCollection services, Action<EasyCachingOptions> action)
        {
            services.TryAddScoped<ICache, CacheManager>();
            services.AddEasyCaching(action);
        }
    }
}