namespace Inbox.ServiceDiscovery.LoadBalancers
{
    /// <summary>
    /// 轮流获取策略
    /// </summary>
    public class RoundRobinLoadBalancer : ILoadBalancer
    {
        private readonly object _lock = new object();
        private int _index = 0;

        public ServiceInformation Load(IList<ServiceInformation> services)
        {
            lock (_lock)
            {
                if (_index >= services.Count)
                {
                    _index = 0;
                }
                return services[_index++];
            }
        }
    }
}
