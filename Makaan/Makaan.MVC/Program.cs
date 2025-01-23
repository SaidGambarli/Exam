using Makaan.MVC.Context;
using Makaan.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddDbContext<MakaanDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
        });
        builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequiredLength = 3;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Lockout.MaxFailedAccessAttempts = 3;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<MakaanDbContext>();

        builder.Services.ConfigureApplicationCookie(opt =>
        {
            opt.LoginPath = "/login";
            opt.AccessDeniedPath = "/Home/AccessDenied";
        });
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
               name: "login",
               pattern: "login", new
               {
                   Controller = "Account",
                   Action = "Login"
               });
        app.MapControllerRoute(
            name: "register",
            pattern: "register", new
            {
                Controller = "Account",
                Action = "Register"
            });
        app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
