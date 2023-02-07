using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class DeletePrincipalConfig : IEntityTypeConfiguration<DeletePrincipal>
    {
        public void Configure(EntityTypeBuilder<DeletePrincipal> entity)
        {
            entity.HasOne(p => p.DependentSetNull)
                .WithOne()
                .HasForeignKey<DeleteDependentSetNull>(p => p.DeletePrincipalId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.DependentRestrict)
                .WithOne()
                .HasForeignKey<DeleteDependentRestrict>(p => p.DeletePrincipalId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.DependentCascade)
                .WithOne()
                .HasForeignKey<DeleteDependentCascade>(p => p.DeletePrincipalId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
