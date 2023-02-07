using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class EmployeeShortFkConfig : IEntityTypeConfiguration<EmployeeShortFk>
    {
        public void Configure(EntityTypeBuilder<EmployeeShortFk> entity)
        {
            entity
                .HasOne(p => p.Manager)
                .WithOne()
                .HasForeignKey<EmployeeShortFk>(p => p.ManagerId);
        }
    }
}
