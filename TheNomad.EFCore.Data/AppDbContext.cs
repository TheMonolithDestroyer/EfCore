using Microsoft.EntityFrameworkCore;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }            
        public DbSet<Author> Authors { get; set; }        
        public DbSet<PriceOffer> PriceOffers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(p => p.BookId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().Property(p => p.AuthorId).ValueGeneratedOnAdd();
            modelBuilder.Entity<PriceOffer>().Property(p => p.PriceOfferId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>().Property(p => p.ReviewId).ValueGeneratedOnAdd();
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
        }
    }
}
