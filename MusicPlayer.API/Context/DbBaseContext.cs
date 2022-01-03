using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models;

namespace MusicPlayer.API
{
    public class DbBaseContext : DbContext
    {
        public DbBaseContext(DbContextOptions<DbBaseContext> options) : base(options)
        { }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}
