namespace Inbox.ServiceDiscovery.Consul
{
    public class ConsulOptions
    {
        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulUrl { get; set; }

        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// 健康检查的地址
        /// </summary>
        public string HealthCheckUrl { get; set; }

        /// <summary>
        /// 心跳间隔
        /// </summary>
        public int HealthCheckIntervalInSecond { get; set; }

        /// <summary>
        /// 服务tag
        /// </summary>
        public string[] ServiceTags { get; set; }

        /// <summary>
        /// ConsulKey路径
        /// </summary>
        public string ConsulKeyPath { get; set; }
    }
}
