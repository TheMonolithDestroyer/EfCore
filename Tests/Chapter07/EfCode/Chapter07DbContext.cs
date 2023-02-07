using System;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.EfClasses;
using Tests.Chapter07.EfCode.Configurations;
using TheNomad.EFCore.Data.Entities;

namespace Tests.Chapter07.EfCode
{
    public class Chapter07DbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeShortFk> EmployeeShortFks { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<LibraryBook> LibraryBooks { get; set; }

        //One-to-One versions
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        //Table-per-hierarchy
        public DbSet<Payment> Payments { get; set; } //#A
        public DbSet<SoldIt> SoldThings { get; set; } //#B

        //Backing fields on relationships
        public DbSet<Ch07Book> Books { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }

        //delete behavior
        public DbSet<DeletePrincipal> DeletePrincipals { get; set; }

        public Chapter07DbContext(DbContextOptions<Chapter07DbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttendeeConfig());
            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new EmployeeShortFkConfig());
            modelBuilder.ApplyConfiguration(new Ch07BookConfig());
            modelBuilder.ApplyConfiguration(new DeletePrincipalConfig());
            modelBuilder.ApplyConfiguration(new PaymentConfig()); //#C
        }

        /**TPH**************************************************
        #A This defines the property through which I can access all the payments, both PaymentCash and PaymentCard
        #B This is the list of sold items, with a required link to a Payment
        #C I call the configureration code for the payment TPH via its extension method, Configure
         * ******************************************************/
    }
}