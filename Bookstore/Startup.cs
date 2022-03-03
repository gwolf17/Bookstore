using Bookstore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>     //Add in the database service
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);   //Connect to your connection string
            });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();  //Set up the repositories
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();  //Set up Purchase repositories

            services.AddRazorPages();  //Add to use Razor Pages

            services.AddDistributedMemoryCache();
            services.AddSession();  //Add these lines to use sessions

            //When we see a Cart object, call the SessionCart method which will get the current cart or create a new one if there isn't one
            services.AddScoped<Cart>(x => SessionCart.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();  //Add to use sessions

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //Route for if a category and page # are selected
                endpoints.MapControllerRoute("categorypage",
                    "{categoryType}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //Route for if just a page # is selected
                endpoints.MapControllerRoute("page",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                //Route for if just a category type is selected
                endpoints.MapControllerRoute("category",
                    "{categoryType}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                //Default controller route if no parameters are passed
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();  //Add endpoint for razor pages
            });
        }
    }
}
