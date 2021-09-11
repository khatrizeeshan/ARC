using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ARC.Persistance;

namespace ARC.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Load(host);
            host.Run();
        }

        private static void Load(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                try
                {
                    provider.InitializeDb();
                }
                catch (Exception ex)
                {
                    var logger = provider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred on loading.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
