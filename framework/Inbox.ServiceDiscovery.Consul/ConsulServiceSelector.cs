using Inbox.ServiceDiscovery.LoadBalancers;

namespace Inbox.ServiceDiscovery.Consul
{
    public class ConsulServiceSelector : IServiceSelector
    {
        /// <summary>
        /// 选取服务节点
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="loadBalancer">负载均衡器</param>
        /// <returns></returns>
        public ServiceInformation Select(IEnumerable<ServiceInformation> services, ILoadBalancer loadBalancer)
        {
            return loadBalancer.Load(services.ToList());
        }
    }
}
