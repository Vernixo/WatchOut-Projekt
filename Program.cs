using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchOut.Areas.Identity.Data;
using WatchOut.Data;
namespace WatchOut
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("WatchOutContextConnection") ?? throw new InvalidOperationException("Connection string 'WatchOutContextConnection' not found.");

            builder.Services.AddDbContext<WatchOutContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<WatchOutUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WatchOutContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
