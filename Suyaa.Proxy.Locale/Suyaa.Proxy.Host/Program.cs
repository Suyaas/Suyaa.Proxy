// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Suyaa;
using Suyaa.Proxy.Host;
using System.Diagnostics;

sy.Logger.GetCurrentLogger()
    .Use((string message) =>
    {
        Debug.WriteLine(message);
    });

string key = "ASPNETCORE_ENVIRONMENT";
if (Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) Environment.SetEnvironmentVariable(key, "Production");
var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables(prefix: "ASPNETCORE_")
               .AddCommandLine(args);
var config = builder.Build();
Suyaa.Hosting.WebHost.CreateHostBuilder<Startup>(webBuilder => webBuilder.UseConfiguration(config), args).Build().Run();
