namespace Inbox.ServiceDiscovery
{
    public interface IServiceRegistry
    {
        /// <summary>
        /// 获取服务Id
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        string GetServiceId(string serviceName, string host, int port);

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="healthCheckUrl"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        Task<ServiceInformation> RegisterServiceAsync(string serviceName, string serviceUrl, string healthCheckUrl, IEnumerable<string> tags = null);

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        Task DeregisterServiceAsync(string serviceId);
    }
}
