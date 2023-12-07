using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using TrollMarket.Provider.Abstraction;
using TrollMarket.Provider.Implementation;

namespace TrollMarket.Web.UI
{
    public class Program
    {
  
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IShipmentService, ShipmentService>();
            builder.Services.AddTransient<IProfileService, ProfileService>();
            builder.Services.AddTransient<IHistoryService, HistoryService>();
            builder.Services.AddTransient<IMerchandiseService, MerchandiseService>();
            builder.Services.AddTransient<IShopService, ShopService>();
            builder.Services.AddTransient<ICartService, CartService>();

       
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(
           option => {
               option.LoginPath = "/Account/Login";
               option.AccessDeniedPath = "/Account/AccessDenied";

           });


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}