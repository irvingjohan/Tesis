using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIEGO.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost = BuildWebHost(args);

            using (var scope = webhost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ApplicationDBContext>();
                context.Database.Migrate();
            }
            webhost.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseConfiguration(new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddCommandLine(args)
                  .Build())
              .UseStartup<Startup>()
              .Build();


    }
}
