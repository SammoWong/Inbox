namespace Inbox.ServiceDiscovery.LoadBalancers
{
    /// <summary>
    /// 负载均衡器
    /// </summary>
    public interface ILoadBalancer
    {
        /// <summary>
        /// 根据策略加载服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        ServiceInformation Load(IList<ServiceInformation> services);
    }
}
