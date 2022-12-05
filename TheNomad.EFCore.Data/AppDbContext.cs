using Microsoft.EntityFrameworkCore;
using System;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }            
        public DbSet<Author> Authors { get; set; }        
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Order> Orders { get; set; } //#A

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { }

        public AppDbContext GetAppDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql($@"Server=localhost; port=5432; user id=postgres; password=pdMsWFjZ; database=EF; pooling=true;SearchPath=public");

            return new AppDbContext(optionsBuilder.Options);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.BookId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>()
                .Property(p => p.AuthorId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<PriceOffer>()
                .Property(p => p.PriceOfferId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>()
                .Property(p => p.ReviewId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<BookAuthor>()
                .HasKey(x => new { x.BookId, x.AuthorId });
            modelBuilder.Entity<Order>()
                .Property(p => p.OrderId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<LineItem>()         //#B
                .HasOne(p => p.ChosenBook)          //#B
                .WithMany()                         //#B
                .OnDelete(DeleteBehavior.Restrict); //#B
            modelBuilder.Entity<Book>()
                .HasQueryFilter(p => !p.SoftDeleted);
        }
    }
}
