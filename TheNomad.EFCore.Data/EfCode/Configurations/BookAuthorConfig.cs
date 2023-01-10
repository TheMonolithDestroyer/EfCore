using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode.Configurations
{
    internal class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> entity)
        {
            entity.HasKey(p => new { p.BookId, p.AuthorId });

            entity.HasOne(pt => pt.Book)
                .WithMany(p => p.AuthorsLink)
                .HasForeignKey(pt => pt.BookId);

            entity.HasOne(pt => pt.Author)
                .WithMany(p => p.BooksLink)
                .HasForeignKey(pt => pt.AuthorId);
        }
    }
}
