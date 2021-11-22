using Inbox.ServiceDiscovery.LoadBalancers;

namespace Inbox.ServiceDiscovery
{
    public interface IServiceSelector
    {
        /// <summary>
        /// 选取服务节点
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="loadBalancer">负载均衡器</param>
        /// <returns></returns>
        ServiceInformation Select(IEnumerable<ServiceInformation> services, ILoadBalancer loadBalancer);
    }
}
