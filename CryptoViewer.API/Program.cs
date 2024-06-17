using System.Text;
using CryptoViewer.Auth_API;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.Auth_API.Repository;
using CryptoViewer.Auth_API.Repository.IRepository;
using CryptoViewer.BL.Auth;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.Crypto.Interface;
using CryptoViewer.DAL;
using CryptoViewer.DAL.Auth;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Crypto;
using CryptoViewer.DAL.Crypto.Interfaces;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CryptoViewer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

            // BL
            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddScoped<IEncrypt, Encrypt>();
            builder.Services.AddScoped<IDbSession, DbSession>();
            builder.Services.AddScoped<ICrypto, Crypto>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            // DAL
            builder.Services.AddScoped<IDbHelper, DbHelper>();
            builder.Services.AddScoped<ILoggingService, LoggingService>();
            builder.Services.AddScoped<IAuthDAL, AuthDAL>();
            builder.Services.AddScoped<IDbSessionDAL, DbSessionDAL>();
            builder.Services.AddScoped<ICryptocurrencyDAL, CryptocurrencyDAL>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("Authorization")),
                ServiceLifetime.Scoped);

            var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Add controllers
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API V1",
                    Description = "API ??? ?????????? ????????"
                });
            });
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

              //  app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                DbHelper.ConnString = app.Configuration["ConnectionStrings:Prod"] ?? "";

                app.Run();
            }
    }
    }
