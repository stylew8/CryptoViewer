using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoViewer.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbHelper and CryptocurrencyRepository services
            builder.Services.AddSingleton<IDbHelper, DbHelper>(sp =>
            {
                var dbHelper = new DbHelper();
                DbHelper.ConnString = builder.Configuration.GetConnectionString("DefaultConnection");
                return dbHelper;
            });
            builder.Services.AddTransient<CryptocurrencyRepository>();

            // Add data protection services with default configuration
            builder.Services.AddDataProtection()
                .SetApplicationName("CryptoViewer");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

          //  app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
