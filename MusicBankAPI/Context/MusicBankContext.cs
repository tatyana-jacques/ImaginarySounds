using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Models;


namespace MusicBankAPI.Context
{
    public class MusicBankContext: DbContext
    {
        public MusicBankContext(DbContextOptions<MusicBankContext> options) : base(options) { }

        public DbSet <Artist> Artists { get; set; }
        public DbSet <Composer> Composers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; } 
        public DbSet<UserSongs> UserSongs { get; set; }
        public DbSet<SongTags> SongTags { get; set; }
        public DbSet<StatusTable> StatusTable { get; set; }
       
    }
}
