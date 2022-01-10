using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MusicPlayer.API.Services;
using MusicPlayer.Authtificate;
using MusicPlayer.BLL.Services;
using MusicPlayer.DAL;
using MusicPlayer.Logics;
using System;
using System.Text;

namespace MusicPlayer.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfiguration = configuration.GetSection("Auth").Get<AuthOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = authConfiguration.Issuer,
                            ValidAudience = authConfiguration.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Secret)),
                            ClockSkew = TimeSpan.Zero,
                        };
                    }
                );
        }

        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfiguration = configuration.GetSection("Auth");
            services.Configure<AuthOptions>(authConfiguration);
        }

        public static void RegisterCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(x => new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())).CreateMapper());
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<AuthService>();
            services.AddTransient<AccountService>();
            services.AddTransient<GenreService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<PlaylistService>();
            services.AddTransient<TrackService>();
        }
    }
}
