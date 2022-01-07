using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.Authtificate;
using MusicPlayer.DAL;

namespace MusicPlayer.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
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
    }
}
