using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode.Configurations
{
    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.Property(p => p.ReviewId)
                .ValueGeneratedOnAdd();
        }
    }
}
