using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Warsys.Data;
using Warsys.Data.Models;
using System.Data;
using Warsys.Services;

namespace Warsys.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WarsysDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<WarsysUser, IdentityRole>(options =>
                    options.SignIn.RequireConfirmedAccount = false) 
                .AddEntityFrameworkStores<WarsysDbContext>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            // Application services.
            services.AddTransient<ISeederService, SeederService>();
            services.AddTransient<ITransactionsService, TransactionsService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Adding Roles and FlowDirections 
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<WarsysDbContext>())
                {
                    //context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        var admin = new IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        };
                        var user = new IdentityRole
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        };
                        context.Roles.Add(admin);
                        context.Roles.Add(user);

                        context.SaveChanges();
                    }

                    if (!context.FlowDirections.Any())
                    {
                        var intake = new FlowDirection
                        {
                            Direction = "INTAKE"
                        };
                        var outflow = new FlowDirection
                        {
                            Direction = "OUTPUT"
                        };
                        context.FlowDirections.Add(intake);
                        context.FlowDirections.Add(outflow);

                        context.SaveChanges();
                    }
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
