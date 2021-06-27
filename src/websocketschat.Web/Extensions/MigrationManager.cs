using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using websocketschat.Database.Context;

namespace websocketschat.Web.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost webHost)
        {
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                using (NpgSqlContext dbContext = scope.ServiceProvider.GetRequiredService<NpgSqlContext>())
                {
                    try
                    {
                        dbContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("DataBase Migration failed with " + ex.Message);
                        throw;
                    }
                }
            }

            return webHost;
        }
    }
}
