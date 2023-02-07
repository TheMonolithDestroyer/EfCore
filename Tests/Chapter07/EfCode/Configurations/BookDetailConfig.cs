using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Chapter07.SplitOwnClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    // table splitting mapping configuration
    public class BookDetailConfig : IEntityTypeConfiguration<BookDetail>
    {
        public void Configure(EntityTypeBuilder<BookDetail> entity)
        {
            entity.ToTable("Books");
        }
    }
}
