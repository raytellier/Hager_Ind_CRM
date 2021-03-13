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

                //Companies Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Companies.Delete));

                //Contacts Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Contacts.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Contacts.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Contacts.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Contacts.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Contacts.Delete));

                //Employees Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Delete));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Employees.Privacy));

                //Users Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Users.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Users.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Users.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Users.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Users.Delete));

                //Roles Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Roles.Create));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Roles.Read));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Roles.Detail));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Roles.Update));
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Roles.Delete));

                //Lists Claim
                await RoleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Lists.Manage));
            }

            //Supervisor Role
            var supervisorExist = await RoleManager.RoleExistsAsync("Supervisor");
            if (!supervisorExist)
            {
                var supRole = new IdentityRole("Supervisor");
                await RoleManager.CreateAsync(supRole);

                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Companies.Read));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Companies.Detail));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Companies.Create));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Companies.Update));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Companies.Delete));

                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Contacts.Read));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Contacts.Detail));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Contacts.Create));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Contacts.Update));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Contacts.Delete));

                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Employees.Read));
                await RoleManager.AddClaimAsync(supRole, new Claim(CustomClaimTypes.Permission, Employees.Detail));
            }

            //Employee Role
            var employeeExist = await RoleManager.RoleExistsAsync("Employee");
            if (!employeeExist)
            {
                var empRole = new IdentityRole("Employee");
                await RoleManager.CreateAsync(empRole);

                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Companies.Read));
                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Companies.Detail));

                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Contacts.Read));
                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Contacts.Detail));

                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Employees.Read));
                await RoleManager.AddClaimAsync(empRole, new Claim(CustomClaimTypes.Permission, Employees.Detail));
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
            if (userManager.FindByEmailAsync("supervisor1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "supervisor1@outlook.com",
                    Email = "supervisor1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Supervisor").Wait();
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
