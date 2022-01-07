using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL.Entities;
using System.Linq;

namespace MusicPlayer.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlaylistTrack>()
                .HasOne(x => x.Track)
                .WithMany(x => x.Playlists)
                .HasForeignKey(x => x.TrackId)
                .IsRequired();

            builder.Entity<PlaylistTrack>()
                .HasOne(x => x.Playlist)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.PlaylistId)
                .IsRequired();

            foreach (var x in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                x.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
