using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode.Configurations
{
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entity)
        {
            entity.Property(p => p.AuthorId)
                .ValueGeneratedOnAdd();
        }
    }
}
