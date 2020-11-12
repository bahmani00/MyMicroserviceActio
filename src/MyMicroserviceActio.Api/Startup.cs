using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMicroserviceActio.Api.Repositories;
using MyMicroserviceActio.Common.Auth;
using MyMicroserviceActio.Common.Events;
using MyMicroserviceActio.Common.Mongo;
using MyMicroserviceActio.Common.RabbitMq;
using MyMicroserviceActio.Common.SeedWork;

namespace MyMicroserviceActio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddJwt(Configuration);

            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
