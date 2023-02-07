using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class Ch07BookConfig : IEntityTypeConfiguration<Ch07Book>
    {
        public void Configure (EntityTypeBuilder<Ch07Book> entity)
        {
            entity.HasKey(p => p.BookId);

            //see https://github.com/aspnet/EntityFramework/issues/6674
            entity.Metadata //#A
                .FindNavigation(nameof(Ch07Book.Reviews)) //#B
                .SetPropertyAccessMode
                    (PropertyAccessMode.Field); //#C
        }
        /******************************************************
        #A Using the MetaData for this entity class I can access some of the deeper features of the entity class
        #B This finds the navigation property using the name of the property
        #C This sets the access mode so that EF Core will ONLY read/write to the backing field
         * ****************************************************/
    }
}
