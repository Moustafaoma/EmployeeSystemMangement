using EmployeeSystemMangement.DAL.Data;
using EmployeeSystemMangement.DAL.Entities;
using EmployeeSystemMangement.PL.Extensions;
using EmployeeSystemMangement.PL.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSystemMangement.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webApplicationBuilder = WebApplication.CreateBuilder(args);
            webApplicationBuilder.Services.AddControllersWithViews();
            webApplicationBuilder.Services.AddDbContext<ApplicationDBContext>(options =>

                 options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnections"))
                 );
            webApplicationBuilder.Services.AddApplicationServicesExtensions();
            webApplicationBuilder. Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));


            webApplicationBuilder.Services.AddIdentity<ApplicationUsers, ApplicationRoles>(options =>
            {
                options.Password.RequiredUniqueChars = 2;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
            webApplicationBuilder. Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
            });
            //Services.AddAuthentication(options =>
            //{
            //	options.DefaultAuthenticateScheme = "MySchema";
            //}); //it if iwant Add Configuration of my custom schema token
            var app = webApplicationBuilder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }


    }


}

