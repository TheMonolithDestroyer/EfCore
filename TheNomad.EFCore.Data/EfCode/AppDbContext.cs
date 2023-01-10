using Microsoft.EntityFrameworkCore;
using System;
using TheNomad.EFCore.Data.EfCode.Configurations;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode
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
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BookAuthorConfig());
            modelBuilder.ApplyConfiguration(new PriceOfferConfig());
            modelBuilder.ApplyConfiguration(new LineItemConfig());
            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
        }
    }
}
