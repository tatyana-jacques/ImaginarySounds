using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Models;


namespace MusicBankAPI.Context
{
    public class MusicBankContext: DbContext
    {
        public MusicBankContext(DbContextOptions<MusicBankContext> options) : base(options) { }

        public DbSet <ArtistViewModel> Artists { get; set; }
        public DbSet <ComposerViewModel> Composers { get; set; }
        public DbSet<SongViewModel> Songs { get; set; }
        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<UserSongsViewModel> UserSongs { get; set; }
        public DbSet<SongTags> SongTags { get; set; }
        public DbSet<MusicBankAPI.Models.TagViewModel> Tag { get; set; } = default!;
    }
}
