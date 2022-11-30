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
            modelBuilder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
        }
    }
}
