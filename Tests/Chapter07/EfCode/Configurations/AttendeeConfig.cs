using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class AttendeeConfig : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> entity)
        {
            entity.HasOne(p => p.Ticket)
                .WithOne(p => p.Attendee)
                .HasForeignKey<Attendee>(p => p.TicketId)
                .IsRequired(true);

            entity.HasOne(p => p.Optional)
                .WithOne(p => p.Attend)
                .HasForeignKey<Attendee>("OptionalTrackId")
                .IsRequired(false);

            entity.HasOne(p => p.Required)
                .WithOne(p => p.Attend)
                .HasForeignKey<Attendee>("MyShadowFk")
                .IsRequired(true);
        }
    }
}