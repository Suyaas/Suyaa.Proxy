using Microsoft.AspNetCore.Http;
using Suyaa.Proxy.Basic.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Basic.Middlewares
{
    /// <summary>
    /// 代理中间件
    /// </summary>
    public class ProxyMiddleware
    {
        // 变量
        private RequestDelegate _next;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="next"></param>
        public ProxyMiddleware(
            RequestDelegate next,
            IServiceProvider provider
            )
        {
            _next = next;
            _provider = provider;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            string url = (context.Request.Path.Value ?? "/").ToLower();
            if (url == "/proxy")
            {
                // 执行代理
                using (Proxies.Proxy proxy = new Proxies.Proxy(_provider))
                {
                    await proxy.Invoke(context);
                }
            }
            return;
            //await _next(context);
        }
    }
}
