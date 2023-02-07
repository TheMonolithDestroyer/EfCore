using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.EfClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        // table per hierarchy mapping configuration
        // allows a set of inherited classes to be saved into one table
        public void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.HasDiscriminator(b => b.PType) //#A
                .HasValue<PaymentCash>(PTypes.Cash) //#B
                .HasValue<PaymentCard>(PTypes.Card); //#C

            //This is needed for TestChangePaymentTypeOk to work - see EF Core issue #7510
            entity.Property(p => p.PType).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        }
    }
    /*******************************************
    #A The HasDiscriminator method idetifies the entity as a TPH and then selects the property PType as the discriminator for the different types. In this case it is an enum, which I have set to be byte in size
    #B This sets the discriminator value for the PaymentCash type
    #C This sets the discriminator value for the PaymentCard type
        * *******************************************/
}
