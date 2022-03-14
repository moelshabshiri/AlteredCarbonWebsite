using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<KitOrder>
    {
        public void Configure(EntityTypeBuilder<KitOrder> builder)
        {
            builder.HasMany(o => o.OrderHistories).WithOne().OnDelete(DeleteBehavior.Cascade);
            // builder.OwnsOne(o => o.ShipToAddress, a => 
            // {
            //     a.WithOwner();
            // });
        }
        
    }
}