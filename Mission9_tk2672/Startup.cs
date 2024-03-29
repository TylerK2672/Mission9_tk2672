using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission9_tk2672.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]);
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();

            //allows razor pages
            services.AddRazorPages();

            // allows session
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // corresponds to wwwroot
            app.UseStaticFiles();
            // allows session
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "categoryPage",
                    "{category}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });
                
                endpoints.MapControllerRoute(
                    name: "page",
                    pattern: "page{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1});

                endpoints.MapControllerRoute(
                    "category",
                    "{category}",
                    new { Controller = "Home", action = "Index", pageNum = 1});

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
