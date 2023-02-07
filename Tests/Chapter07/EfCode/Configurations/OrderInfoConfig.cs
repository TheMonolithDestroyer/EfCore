using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tests.Chapter07.SplitOwnClasses;

namespace Tests.Chapter07.EfCode.Configurations
{
    // owned type mapping configuration
    // a class to be merged into the entity class's table
    public class OrderInfoConfig : IEntityTypeConfiguration<OrderInfo>
    {
        public void Configure(EntityTypeBuilder<OrderInfo> entity)
        {
            entity
                .OwnsOne(p => p.BillingAddress);

            entity
                .OwnsOne(p => p.DeliveryAddress);
        }
        /*******************************************************************
         * ********************************************************************/
    }
}
