using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Seeds the Identity database with data
namespace Bookstore.Models
{
    public static class IdentitySeedData  //Change to static class
    {
        //Set up constant variables
        public const string adminUser = "Admin";
        public const string adminPassword = "413ExtraYeetPeriod(t)!";

        //Make sure there is data in the database to begin with
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDBContext context = app.ApplicationServices   //Instance of the context file
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();

            //Run pending migrations automatically
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            //If the user is null, set the attributes
            if (user == null)
            {
                user = new IdentityUser(adminUser);  //Create a new user

                user.Email = "admin@yeet.com";
                user.PhoneNumber = "111-222";

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
