using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.DAL;

namespace MusicPlayer.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AutoMigrateDb(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
        }
    }
}
