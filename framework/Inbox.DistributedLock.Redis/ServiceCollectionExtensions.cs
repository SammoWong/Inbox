using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.DistributedLock.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisDistributedLock(this IServiceCollection services, DistributedLockOption option)
        {
            return services.AddSingleton(sp => { return new DistributedLockManager(option.Configuration); });
        }
    }
}
