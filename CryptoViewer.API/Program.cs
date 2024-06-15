using CryptoViewer.BL.Auth;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.DAL;
using CryptoViewer.DAL.Auth;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoViewer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // BL
            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddScoped<IEncrypt, Encrypt>();
            builder.Services.AddScoped<IDbSession, DbSession>();

            // DAL
            builder.Services.AddScoped<IDbHelper, DbHelper>();
            builder.Services.AddScoped<ILoggingService, LoggingService>();
            builder.Services.AddScoped<IAuthDAL, AuthDAL>();
            builder.Services.AddScoped<IDbSessionDAL, DbSessionDAL>();
            builder.Services.AddScoped<CryptocurrencyRepository>();

            // Add controllers
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

           // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            DbHelper.ConnString = app.Configuration["ConnectionStrings:Prod"] ?? "";

            app.Run();
        }
    }
}
