using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderHistoryConfiguration : IEntityTypeConfiguration<KitOrderHistory>
    {
        public void Configure(EntityTypeBuilder<KitOrderHistory> builder)
        {
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}