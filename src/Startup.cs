using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutTracker.Business;
using WorkoutTracker.Business.Interfacs;
using WorkoutTracker.Controllers.Dto;
using WorkoutTracker.Persistence;
using WorkoutTracker.Persistence.Interfaces;

namespace WorkoutTracker
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
            services.AddDbContext<WorkoutTrackerDbContext>(opt =>
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<ILoginCredentialsRepository, LoginCredentialsRepository>();
            services.AddTransient<IAuthenticationProcessor, AuthenticationProcessor>();
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
        }
    }
}
