using Consul;

namespace Inbox.ServiceDiscovery.Consul
{
    public class ConsulServiceRegistry : IServiceRegistry
    {
        private readonly IConsulClient _consulClient;

        public ConsulServiceRegistry(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        /// <summary>
        /// 获取服务Id
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public string GetServiceId(string serviceName, string host, int port)
        {
            return $"{serviceName}.{host}.{port}";
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="healthCheckUrl"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async Task<ServiceInformation> RegisterServiceAsync(string serviceName, string serviceUrl, string healthCheckUrl, IEnumerable<string> tags = null)
        {
            var serviceUri = new Uri(serviceUrl);
            var serviceId = GetServiceId(serviceName, serviceUri.Host, serviceUri.Port);
            var scheme = serviceUri.Scheme;

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Tags = tags?.ToArray(),
                Address = serviceUri.Host,
                Port = serviceUri.Port,
                Check = new AgentCheckRegistration()
                {
                    HTTP = $"{scheme}://{serviceUri.Host}:{serviceUri.Port}{healthCheckUrl}",
                    //Status = HealthStatus.Passing,
                    Timeout = TimeSpan.FromSeconds(3),
                    Interval = TimeSpan.FromSeconds(10),
                    //服务启动多久后注册
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(3),
                }
            };

            await _consulClient.Agent.ServiceRegister(registration);

            return new ServiceInformation
            {
                Name = registration.Name,
                Id = registration.ID,
                Host = registration.Address,
                Port = registration.Port,
                Tags = tags
            };
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task DeregisterServiceAsync(string serviceId)
        {
            await _consulClient.Agent.ServiceDeregister(serviceId);
        }
    }
}
