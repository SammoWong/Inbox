using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder @this)
        {
            IServiceCollection services = @this.ApplicationServices.GetService<IServiceCollection>();
            var registrar = new AutoMapperRegistrar(services, Assembly.GetExecutingAssembly());
            registrar.CreateMap();

            return @this;
        }
    }
}
