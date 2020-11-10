using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Mongo;
using MyMicroserviceActio.Common.RabbitMq;
using MyMicroserviceActio.Common.SeedWork;
using MyMicroserviceActio.Services.Activities.Domain.Repositories;
using MyMicroserviceActio.Services.Activities.Handlers;
using MyMicroserviceActio.Services.Activities.Repositories;
using MyMicroserviceActio.Services.Activities.Services;

namespace MyMicroserviceActio.Services.Activities
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
            services.AddRabbitMq(Configuration);
            services.AddMongoDB(Configuration);
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //init db
            app.ApplicationServices.GetService<IDatabaseInitializer>()
                .InitializeAsync();
        }
    }
}
