using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Remote.Basic.Helpers
{
    /// <summary>
    /// HttpRequest 助手
    /// </summary>
    public static class HttpRequestHelper
    {
        /// <summary>
        /// 获取内容字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetContentBytes(this HttpRequest request)
        {
            // 获取response
            List<byte> bytes = new List<byte>();
            byte[] buffer = new byte[4096];
            using var stream = request.Body;
            int len = 0;
            do
            {
                len = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (len > 0) bytes.AddRange(buffer.Take(len).ToList());
            } while (len > 0);
            buffer = new byte[0];
            return bytes.ToArray();
        }

        /// <summary>
        /// 获取内容字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<string> GetContentString(this HttpRequest request)
        {
            // 获取response
            List<byte> bytes = new List<byte>();
            byte[] buffer = new byte[4096];
            using var stream = request.Body;
            int len = 0;
            do
            {
                len = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (len > 0) bytes.AddRange(buffer.Take(len).ToList());
            } while (len > 0);
            buffer = new byte[0];
            string content = Encoding.ASCII.GetString(bytes.ToArray());
            bytes.Clear();
            return content;
        }
    }
}
