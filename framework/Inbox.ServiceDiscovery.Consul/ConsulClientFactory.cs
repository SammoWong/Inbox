using Consul;

namespace Inbox.ServiceDiscovery.Consul
{
    public class ConsulClientFactory
    {
        /// <summary>
        /// 创建并返回Consul客户端
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IConsulClient Create(Uri address)
        {
            return new ConsulClient(p =>
            {
                p.Address = address;
            });
        }
    }
}
