using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.EfCode.Configurations;
using Tests.Chapter07.SplitOwnClasses;

namespace Tests.Chapter07.EfCode
{
    public class SplitOwnDbContext : DbContext
    {
        public DbSet<BookSummary> BookSummaries { get; set; }
        public DbSet<OrderInfo> Orders { get; set; }

        public SplitOwnDbContext(
            DbContextOptions<SplitOwnDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookSummaryConfig());
            modelBuilder.ApplyConfiguration(new BookDetailConfig());
            modelBuilder.ApplyConfiguration(new OrderInfoConfig());
        }
    }
}
