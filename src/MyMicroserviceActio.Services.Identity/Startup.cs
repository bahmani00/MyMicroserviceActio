using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyMicroserviceActio.Common.Auth;
using MyMicroserviceActio.Common.Commands;
using MyMicroserviceActio.Common.Mongo;
using MyMicroserviceActio.Common.RabbitMq;
using MyMicroserviceActio.Common.SeedWork;
using MyMicroserviceActio.Services.Identity.Domain.Repositories;
using MyMicroserviceActio.Services.Identity.Domain.Services;
using MyMicroserviceActio.Services.Identity.Handlers;
using MyMicroserviceActio.Services.Identity.Repositories;
using MyMicroserviceActio.Services.Identity.Services;

namespace MyMicroserviceActio.Services.Identity
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
            services.AddJwt(Configuration);

            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
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
