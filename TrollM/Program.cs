using Microsoft.AspNetCore.Cors.Infrastructure;
using TrollMarket.Provider.Abstraction;
using TrollMarket.Provider.Implementation;

namespace TrollM
{
    public class Program
    {
        public static void CorsWhiteList(CorsOptions options)
        {
            options.AddPolicy(
                name: "AllowAnyOrigin",
                policy => {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                }
                );
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IShopService, ShopService>();

            builder.Services.AddCors(CorsWhiteList);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}