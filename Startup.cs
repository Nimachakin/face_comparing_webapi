using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FaceRecognitionApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FaceRecognitionApi
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
            services.AddDbContext<WantedTestPrivateContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting(); 
            app.UseMvc(configureroutes => {
                configureroutes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");
                configureroutes.MapRoute(
                    name: "api",
                    template: "api/{controller=facesearch}/{action=getphotostring}/{id?}");
            });          
        }
    }
}
