﻿using Inbox.ServiceDiscovery.LoadBalancers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inbox.ServiceDiscovery.Consul
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Consul
        /// </summary>
        /// <param name="services"></param>
        /// <param name="consulUrl"></param>
        /// <param name="loadBalancer"></param>
        public static void AddConsul(this IServiceCollection services, string consulUrl, ILoadBalancer loadBalancer)
        {
            var consulUri = new Uri(consulUrl);
            services.AddSingleton<IServiceRegistry>(provider =>
            {
                return new ConsulServiceRegistry(ConsulClientFactory.Create(consulUri));
            });
            services.AddSingleton<IServiceDiscovery>(provider =>
            {
                return new ConsulServiceDiscovery(ConsulClientFactory.Create(consulUri));
            });
            services.AddSingleton(typeof(ILoadBalancer), loadBalancer);
            services.AddSingleton<IServiceSelector, ConsulServiceSelector>();
        }

        /// <summary>
        /// 服务注册到Consul
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void RegisterToConsul(this IApplicationBuilder app, ConsulOptions options)
        {
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var serviceRegistry = app.ApplicationServices.GetRequiredService<IServiceRegistry>();

            var serviceUri = GetServiceAddressInternal(app, options);
            var serviceId = serviceRegistry.GetServiceId(options.ServiceName, serviceUri.Host, serviceUri.Port);

            lifetime.ApplicationStarted.Register(async () =>
            {
                await serviceRegistry.RegisterServiceAsync(options.ServiceName, options.ServiceUrl, options.HealthCheckUrl, options.ServiceTags);
            });
            lifetime.ApplicationStopping.Register(async () =>
            {
                await serviceRegistry.DeregisterServiceAsync(serviceId);
            });
        }

        /// <summary>
        /// 获取服务地址
        /// </summary>
        /// <param name="app"></param>
        /// <param name="consulOption"></param>
        /// <returns></returns>
        private static Uri GetServiceAddressInternal(IApplicationBuilder app, ConsulOptions options)
        {
            var errorMsg = string.Empty;
            Uri serviceAddress = default;

            if (options == null)
                throw new Exception("请正确配置Consul");

            if (string.IsNullOrEmpty(options.ConsulUrl))
                throw new Exception("请正确配置ConsulUrl");

            if (string.IsNullOrEmpty(options.ServiceName))
                throw new Exception("请正确配置ServiceName");

            if (string.IsNullOrEmpty(options.HealthCheckUrl))
                throw new Exception("请正确配置HealthCheckUrl");

            if (options.HealthCheckIntervalInSecond <= 0)
                throw new Exception("请正确配置HealthCheckIntervalInSecond");

            //获取网卡所有Ip地址，排除回路地址
            var allIPAddress = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                               .Select(p => p.GetIPProperties())
                               .SelectMany(p => p.UnicastAddresses)
                               .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
                               .Select(p => p.Address.ToString()).ToArray();

            //获取web服务器监听地址，也就是提供访问的地址
            var listenAddresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.ToList();
            List<Uri> listenUrls = new List<Uri>();
            listenAddresses.ForEach(a =>
            {
                var address = a.Replace("[::]", "0.0.0.0")
                               .Replace("+", "0.0.0.0")
                               .Replace("*", "0.0.0.0");

                listenUrls.Add(new Uri(address));
            });

            //第一种注册方式，在配置文件中指定服务地址
            //如果配置了服务地址, 只需要检测是否在listenUrls里面即可
            if (!string.IsNullOrEmpty(options.ServiceUrl))
            {
                serviceAddress = new Uri(options.ServiceUrl);
                bool isExists = listenUrls.Where(p => p.Host == serviceAddress.Host || p.Host == "0.0.0.0").Any();
                if (isExists)
                    return serviceAddress;
                else
                    throw new Exception($"服务{options.ServiceUrl}配置错误 listenUrls={string.Join(',', (IEnumerable<Uri>)listenUrls)}");
            }

            //第二种注册方式，服务地址通过docker环境变量(DOCKER_LISTEN_HOSTANDPORT)指定。
            //可以写在dockerfile文件中，也可以运行容器时指定。运行容器时指定才是最合理的,大家看各自的情况怎么处理吧。
            var dockerListenServiceUrl = Environment.GetEnvironmentVariable("DOCKER_LISTEN_HOSTANDPORT");
            if (!string.IsNullOrEmpty(dockerListenServiceUrl))
            {
                serviceAddress = new Uri(dockerListenServiceUrl);
                return serviceAddress;
            }

            //第三种注册方式，注册程序自动获取服务地址
            //本机所有可用IP与listenUrls进行匹配, 如果listenUrl是"0.0.0.0"或"[::]", 则任意IP都符合匹配
            var matches = allIPAddress.SelectMany(ip =>
                                      listenUrls
                                      .Where(uri => ip == uri.Host || uri.Host == "0.0.0.0")
                                      .Select(uri => new { Protocol = uri.Scheme, ServiceIP = ip, Port = uri.Port })
                                      ).ToList();

            //过滤无效地址
            var filteredMatches = matches.Where(p => !p.ServiceIP.Contains("0.0.0.0")
                                                && !p.ServiceIP.Contains("localhost")
                                                && !p.ServiceIP.Contains("127.0.0.1")
                                                );

            var finalMatches = filteredMatches.ToList();

            //没有匹配的地址,抛出异常
            if (finalMatches.Count() == 0)
                throw new Exception($"没有匹配的Ip地址=[{string.Join(',', allIPAddress)}], urls={string.Join(',', (IEnumerable<Uri>)listenUrls)}");

            //只有一个匹配,直接返回
            if (finalMatches.Count() == 1)
            {
                serviceAddress = new Uri($"{finalMatches[0].Protocol}://{ finalMatches[0].ServiceIP}:{finalMatches[0].Port}");
                return serviceAddress;
            }

            //匹配多个，直接放回第一个
            serviceAddress = new Uri($"{finalMatches[0].Protocol}://{ finalMatches[0].ServiceIP}:{finalMatches[0].Port}");
            return serviceAddress;
        }
    }
}
