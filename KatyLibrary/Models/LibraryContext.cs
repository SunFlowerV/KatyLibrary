using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KatyLibrary.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreBook>()
                .HasKey(t => new { t.GenreId, t.BookId });

            modelBuilder.Entity<GenreBook>()
                .HasOne(sc => sc.Genre)
                .WithMany(s => s.GenreBooks)
                .HasForeignKey(sc => sc.GenreId);

            modelBuilder.Entity<GenreBook>()
                .HasOne(sc => sc.Book)
                .WithMany(c => c.GenreBooks)
                .HasForeignKey(sc => sc.BookId);
            modelBuilder.Entity<Book>().Property(u => u.IsBestSeller).HasDefaultValue(false);
            modelBuilder.Entity<Author>().Property(u => u.Foto).HasDefaultValue("/images/NotAvatar.png");
            modelBuilder.Entity<Book>().Property(u => u.BookCover).HasDefaultValue("/images/NotCover.png");
        }
    }
}
