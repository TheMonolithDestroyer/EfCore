using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.SplitOwnClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class BookSummaryConfig : IEntityTypeConfiguration<BookSummary>
    {
        // table splitting mapping configuration
        public void Configure(EntityTypeBuilder<BookSummary> entity)
        {
            entity
                .HasOne(e => e.Details).WithOne()
                .HasForeignKey<BookDetail>(e => e.BookDetailId);
            
            entity.ToTable("Books");
        }
    }
}
