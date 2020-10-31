using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MyMicroserviceActio.Common.Events;
using MyMicroserviceActio.Common.Services;

namespace MyMicroserviceActio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
               .UseRabbitMq()
               .SubscribeToEvent<ActivityCreated>()
               .Build()
               .Run();
        }

        //private static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
