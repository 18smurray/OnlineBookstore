using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookstore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Pass connection info for connecting to database using connection string
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:OnlineBookstoreConnection"]);
            });

            //Each session gets its own repository object - takes service and implementation 
            services.AddScoped<iBookstoreRepository, EFBookstoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Custom url for when user inputs category and page number
                endpoints.MapControllerRoute(
                    "catandpage",
                    "{category}/{page:int}",
                    new { Controller = "Home", action = "Index" });

                //Custom url for when user inputs number only
                endpoints.MapControllerRoute(
                    "pageonly",
                    "{page:int}",
                    new { Controller = "Home", action = "Index" });

                //Custom url for when user inputs category only
                endpoints.MapControllerRoute(
                    "catonly",
                    "{category}",
                    new { Controller = "Home", action = "Index", page=1 }); //Set default page as 1

                //Customize the url user sees when navigating to different page numbers of the Index view
                endpoints.MapControllerRoute(
                    "pagination",
                    "P{page:int}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
