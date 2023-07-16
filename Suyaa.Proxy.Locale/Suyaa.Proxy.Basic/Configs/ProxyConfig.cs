using Suyaa.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Proxy.Locale.Basic.Configs
{
    /// <summary>
    /// 代理配置
    /// </summary>
    public class ProxyConfig : IConfig
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 主机配置
        /// </summary>
        public List<ProxyHostConfig> Hosts { get; set; } = new List<ProxyHostConfig>();

        /// <summary>
        /// 默认配置
        /// </summary>
        public void Default()
        {
            this.Url = "{0}";
            //throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 代理配置
    /// </summary>
    public class ProxyHostConfig
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 80;

        /// <summary>
        /// 是否安全通讯
        /// </summary>
        public bool IsHttps { get; set; } = false;
    }
}
