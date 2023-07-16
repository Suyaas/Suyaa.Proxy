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
        /// 默认配置
        /// </summary>
        public void Default()
        {
            this.Url = "{0}";
            //throw new NotImplementedException();
        }
    }
}
