using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Services;

namespace MyMicroserviceActio.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                 .UseRabbitMq()
                 .SubscribeToCommand<CreateUser>()
                 .Build()
                 .Run();

            //CreateHostBuilder(args).Build().Run();
        }

        //private static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
