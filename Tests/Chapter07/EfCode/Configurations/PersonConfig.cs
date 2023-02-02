using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        // Here I use alternate key whick person.UserId. 
        // ContactInfo.EmailAddress is foreign key to person.UserId
        // Alternate key = principal key
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity
                .HasOne(p => p.ContactInfo)
                .WithOne()
                .HasForeignKey<ContactInfo>(p => p.EmailAddress)
                .HasPrincipalKey<Person>(p => p.UserId);
        }
    }
}
