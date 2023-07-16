using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Data;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting;
using Suyaa.Hosting.Dependency;
using Microsoft.EntityFrameworkCore;
using Suyaa.Hosting.EFCores;
using Suyaa.Proxy.Basic.Helpers;

namespace Suyaa.Proxy.Host
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup : Hosting.StartupBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //throw new NotImplementedException();
            app.UseProxy();
        }

        protected override void OnConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialize()
        {
            // 引入App
            //this.Import<Apps.ModuleStartup>();
        }
    }
}