using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System.IO;

namespace SmartCqrs.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).
            ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
            })
            .UseUrls("http://*:8800")
            .ConfigureLogging(logging =>
            {
                //logging.ClearProviders(); // 可以清除所有记录log日志的提供者
                logging.AddNLog();
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseStartup<Startup>();
    }
}
