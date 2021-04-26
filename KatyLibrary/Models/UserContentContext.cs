using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Models
{
    public class UserContentContext : DbContext
    {
        public DbSet<UserBook> UserBooks { get; set; }
        public UserContentContext(DbContextOptions<UserContentContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>()
                .HasKey(t => new { t.UserId, t.BookId });
        }

    }
}
