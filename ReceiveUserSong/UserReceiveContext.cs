using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicBankAPI.Models;
using Process;

namespace Receive
{
    public class UserReceiveContext : DbContext
    {
        public DbSet<UserSongs> UserSongs { get; set; }
        public DbSet<StatusTable> StatusTable { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MusicBank;Trusted_Connection=True;persist security info=True;");

    }
}