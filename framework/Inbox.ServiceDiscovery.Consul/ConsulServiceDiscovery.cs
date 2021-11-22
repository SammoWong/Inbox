using Consul;

namespace Inbox.ServiceDiscovery.Consul
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly IConsulClient _consulClient;

        public ConsulServiceDiscovery(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        /// <summary>
        /// 获取服务信息
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceInformation>> GetServicesAsync(string serviceName)
        {
            var queryResult = await _consulClient.Health.Service(serviceName);

            var services = queryResult.Response.Select(serviceEntry => new ServiceInformation
            {
                Name = serviceEntry.Service.Service,
                Id = serviceEntry.Service.ID,
                Host = serviceEntry.Service.Address,
                Port = serviceEntry.Service.Port,
                Tags = serviceEntry.Service.Tags
            });
            return services;
        }
    }
}
