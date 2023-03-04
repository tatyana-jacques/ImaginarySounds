using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Process;

namespace Process
{
    public class ProcessContext : DbContext

    {
        public DbSet<UserSongs> UserSongs { get; set; }
        public DbSet<StatusTable> StatusTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MusicBank;Trusted_Connection=True;persist security info=True;");
        }

    }
}