﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Logs;
using Suyaa.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Basic.Proxies
{
    /// <summary>
    /// 代理器
    /// </summary>
    public class GetProxy : IDisposable
    {
        // 定义
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 代理器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        public GetProxy(IServiceProvider provider)
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
            var response = context.Response;
            var url = request.Query["url"];
            var uri = new Uri(url);
            var opt = new HttpOption();
            // 设置头
            foreach (var header in request.Headers)
            {
                //if (header.Key == "Accept") continue;
                if (header.Key.StartsWith(":")) continue;
                if (header.Key == "Host") continue;
                opt.Headers.Set(header.Key, header.Value);
                logger.Info($"【Header】{header.Key} = {header.Value}");
            }
            // 设置 cookie
            foreach (var cookie in request.Cookies)
            {
                opt.Cookies.Set(cookie.Key, cookie.Value);
                logger.Info($"【Cookie】{cookie.Key} = {cookie.Value}");
            }
            // 获取response
            using var resp = await sy.Http.GetResponseAsync(url, opt);
            // 设置返回状态
            //response.Clear();
            response.StatusCode = (int)resp.StatusCode;
            if (resp.IsSuccessStatusCode)
            {
                // 处理Content-Type
                var headers = resp.Content.Headers;
                string? contentType = headers.ContentType?.ToString();
                if (!contentType.IsNullOrWhiteSpace()) response.Headers.Add("Content-Type", contentType);
                logger.Info($"【Response.Header】Content-Type = {contentType}");
                string contentEncoding = string.Join(';', headers.ContentEncoding.ToString());
                if (!contentEncoding.IsNullOrWhiteSpace()) response.Headers.Add("Content-Encoding", contentEncoding);
                logger.Info($"【Response.Header】Content-Encoding = {contentEncoding}");
                long? contentLength = headers.ContentLength;
                if (contentLength.HasValue) response.Headers.Add("Content-Length", contentLength.Value.ToString());
                logger.Info($"【Response.Header】Content-Length = {contentLength}");
                string? contentDisposition = headers.ContentDisposition?.ToString();
                if (!contentDisposition.IsNullOrWhiteSpace()) response.Headers.Add("Content-Disposition", contentDisposition);
                logger.Info($"【Response.Header】Content-Disposition = {contentDisposition}");
                byte[] buffer = new byte[4096];
                using var stream = await resp.Content.ReadAsStreamAsync();
                int len = 0;
                do
                {
                    len = stream.Read(buffer, 0, buffer.Length);
                    if (len > 0) await response.Body.WriteAsync(buffer, 0, len);
                } while (len > 0);
                await response.Body.FlushAsync();
                buffer = new byte[0];
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
