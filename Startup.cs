using Hager_Ind_CRM.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hager_Ind_CRM
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<HagerIndContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("HagerIndConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Permissions
            services.AddAuthorization(options =>
            {
                // Company policies
                options.AddPolicy(PolicyTypes.Companies.Create, policy => policy.RequireClaim(CustomClaimTypes.Permission, Companies.Create));
                options.AddPolicy(PolicyTypes.Companies.Read, policy => policy.RequireClaim(CustomClaimTypes.Permission, Companies.Read));
                options.AddPolicy(PolicyTypes.Companies.Detail, policy => policy.RequireClaim(CustomClaimTypes.Permission, Companies.Detail));
                options.AddPolicy(PolicyTypes.Companies.Update, policy => policy.RequireClaim(CustomClaimTypes.Permission, Companies.Update));
                options.AddPolicy(PolicyTypes.Companies.Delete, policy => policy.RequireClaim(CustomClaimTypes.Permission, Companies.Delete));

                // Contact policies
                options.AddPolicy(PolicyTypes.Contacts.Create, policy => policy.RequireClaim(CustomClaimTypes.Permission, Contacts.Create));
                options.AddPolicy(PolicyTypes.Contacts.Read, policy => policy.RequireClaim(CustomClaimTypes.Permission, Contacts.Read));
                options.AddPolicy(PolicyTypes.Contacts.Detail, policy => policy.RequireClaim(CustomClaimTypes.Permission, Contacts.Detail));
                options.AddPolicy(PolicyTypes.Contacts.Update, policy => policy.RequireClaim(CustomClaimTypes.Permission, Contacts.Update));
                options.AddPolicy(PolicyTypes.Contacts.Delete, policy => policy.RequireClaim(CustomClaimTypes.Permission, Contacts.Delete));

                // Employee policies
                options.AddPolicy(PolicyTypes.Employees.Create, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Create));
                options.AddPolicy(PolicyTypes.Employees.Read, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Read));
                options.AddPolicy(PolicyTypes.Employees.Detail, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Detail));
                options.AddPolicy(PolicyTypes.Employees.Update, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Update));
                options.AddPolicy(PolicyTypes.Employees.Delete, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Delete));
                options.AddPolicy(PolicyTypes.Employees.Privacy, policy => policy.RequireClaim(CustomClaimTypes.Permission, Employees.Privacy));

                // Users policies
                options.AddPolicy(PolicyTypes.Users.Create, policy => policy.RequireClaim(CustomClaimTypes.Permission, Users.Create));
                options.AddPolicy(PolicyTypes.Users.Read, policy => policy.RequireClaim(CustomClaimTypes.Permission, Users.Read));
                options.AddPolicy(PolicyTypes.Users.Detail, policy => policy.RequireClaim(CustomClaimTypes.Permission, Users.Detail));
                options.AddPolicy(PolicyTypes.Users.Update, policy => policy.RequireClaim(CustomClaimTypes.Permission, Users.Update));
                options.AddPolicy(PolicyTypes.Users.Delete, policy => policy.RequireClaim(CustomClaimTypes.Permission, Users.Delete));

                // Roles policies
                options.AddPolicy(PolicyTypes.Roles.Create, policy => policy.RequireClaim(CustomClaimTypes.Permission, Roles.Create));
                options.AddPolicy(PolicyTypes.Roles.Read, policy => policy.RequireClaim(CustomClaimTypes.Permission, Roles.Read));
                options.AddPolicy(PolicyTypes.Roles.Detail, policy => policy.RequireClaim(CustomClaimTypes.Permission, Roles.Detail));
                options.AddPolicy(PolicyTypes.Roles.Update, policy => policy.RequireClaim(CustomClaimTypes.Permission, Roles.Update));
                options.AddPolicy(PolicyTypes.Roles.Delete, policy => policy.RequireClaim(CustomClaimTypes.Permission, Roles.Delete));

                // Lists policy
                options.AddPolicy(PolicyTypes.Lists.Manage, policy => policy.RequireClaim(CustomClaimTypes.Permission, Lists.Manage));
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
