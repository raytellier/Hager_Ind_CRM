using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hager_Ind_CRM.Data
{
    public static class ApplicationSeedData
    {
        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //Create Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Admin Role
            var adminExist = await RoleManager.RoleExistsAsync("Admin");
            if (!adminExist)
            {
                var adminRole = new IdentityRole("Admin");
                await RoleManager.CreateAsync(adminRole);

                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Delete));
            }

            //Employee Role
            var employeeExist = await RoleManager.RoleExistsAsync("Employee");
            if (!employeeExist)
            {
                var empRole = new IdentityRole("Employee");
                await RoleManager.CreateAsync(empRole);

                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Companies.Read));
                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Companies.Detail));
            }

            //Create Users
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("admin1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin1@outlook.com",
                    Email = "admin1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("employee1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "employee1@outlook.com",
                    Email = "employee1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Employee").Wait();
                }
            }
            if (userManager.FindByEmailAsync("user1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "user1@outlook.com",
                    Email = "user1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;
                //Not in any role
            }
        }
    }
}
