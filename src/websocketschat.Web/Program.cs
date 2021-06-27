using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using websocketschat.Web.Extensions;

namespace websocketschat.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                        services.AddHostedService<BotBackgroundService>()
                );
    }
}
