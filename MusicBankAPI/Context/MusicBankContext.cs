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
        public DbSet<User>Users { get; set; }
    }
}
