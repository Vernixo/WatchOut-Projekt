using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchOut.Areas.Identity.Data;
using WatchOut.Data;
using WatchOut.Models;
using System;

namespace WatchOut
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("WatchOutContextConnection") ?? throw new InvalidOperationException("Connection string 'WatchOutContextConnection' not found.");
            builder.Services.AddDbContext<WatchOutContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<WatchOutUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<WatchOutContext>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
