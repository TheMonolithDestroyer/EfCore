using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TheNomad.EFCore.Data.Entities;

namespace TheNomad.EFCore.Data.EfCode.Configurations
{
    internal class PriceOfferConfig : IEntityTypeConfiguration<PriceOffer>
    {
        public void Configure(EntityTypeBuilder<PriceOffer> entity)
        {
            entity.Property(p => p.PriceOfferId)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.NewPrice)
                .HasColumnType("decimal(9,2)");
        }
    }
}
