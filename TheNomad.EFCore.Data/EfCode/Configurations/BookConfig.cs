using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode.Configurations
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.Property(p => p.BookId)
                .ValueGeneratedOnAdd();
            entity.Property(p => p.PublishedOn)
                .HasColumnType("date");
            entity.Property(p => p.ImageUrl)
                .IsUnicode(false);
            entity.Property(p => p.Price)
                .HasColumnType("decimal(9,2)");
            entity.HasIndex(p => p.PublishedOn);
            entity.HasQueryFilter(p => !p.SoftDeleted);

            entity.HasOne(p => p.Promotion)
                .WithOne()
                .HasForeignKey<PriceOffer>(p => p.BookId);
            entity.HasMany(p => p.Reviews)
                .WithOne()
                .HasForeignKey(p => p.BookId);
        }
    }
}
