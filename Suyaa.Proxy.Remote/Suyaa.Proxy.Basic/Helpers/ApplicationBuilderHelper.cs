using Microsoft.AspNetCore.Builder;
using Suyaa.Proxy.Basic.Middlewares;
using Suyaa.Proxy.Basic.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Basic.Helpers
{
    /// <summary>
    /// 应用程序构建帮助
    /// </summary>
    public static class ApplicationBuilderHelper
    {
        /// <summary>
        /// 使用令牌交互
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseProxy(this IApplicationBuilder app) => app.UseMiddleware<ProxyMiddleware>();
    }
}
