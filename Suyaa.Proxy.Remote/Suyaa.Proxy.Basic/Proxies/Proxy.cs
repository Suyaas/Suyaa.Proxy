using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Basic.Proxies
{
    /// <summary>
    /// 代理器
    /// </summary>
    public class Proxy : IDisposable
    {
        // 定义
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 代理器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        public Proxy(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var logger = _provider.GetRequiredService<ILogger>();
            var request = context.Request;
            var url = request.Query["url"];
            logger.Info($"[{request.Method}]{request.Path.Value}?url={url}");
            try
            {
                switch (request.Method)
                {
                    case "GET":
                        using (GetProxy proxy = new GetProxy(_provider))
                        {
                            await proxy.Invoke(context);
                        }
                        break;
                    case "POST":
                        using (PostProxy proxy = new PostProxy(_provider))
                        {
                            await proxy.Invoke(context);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error($"{ex}");
            }
        }

        /// <summary>
        /// 释放托管资源
        /// </summary>
        public void Dispose()
        {

        }
    }
}
