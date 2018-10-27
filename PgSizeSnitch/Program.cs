using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PgSizeSnitch.Configurations;
using PgSizeSnitch.Snitch;
using PgSizeSnitch.Snitch.Interfaces;

namespace PgSizeSnitch
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureService(services);

            var serviceProvider = services.BuildServiceProvider();

            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, e) =>
            {
                cts.Cancel();
                e.Cancel = true;
            };

            try
            {
                await serviceProvider.GetService<Application>()
                    .Run(cts.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ConfigureService(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddOptions();
            services.Configure<ApplicationConfig>(configuration.GetSection("Application"));
            services.Configure<GoogleServiceConfig>(configuration.GetSection("GoogleService"));

            services.AddSingleton<IGoogleSheetsService, GoogleSheetsService>();
            services.AddTransient<IPgInfoService, PgInfoService>();

            services.AddTransient<Application>();
        }
    }
}
