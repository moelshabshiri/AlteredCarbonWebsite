using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ItemHistoryConfiguration : IEntityTypeConfiguration<ItemHistory>
    {
        public void Configure(EntityTypeBuilder<ItemHistory> builder)
        {

            builder.HasKey(x => new { x.HistoryId, x.ItemId });

            // builder
            //     .HasOne<KitOrderHistory>(sc => sc.History)
            //     .WithMany(s => s.ItemHistorys)
            //     .HasForeignKey(sc => sc.HistoryId).OnDelete(DeleteBehavior.Cascade);


            // builder
            //     .HasOne<KitOrderItem>(sc => sc.Item)
            //     .WithMany(s => s.ItemHistorys)
            //     .HasForeignKey(sc => sc.ItemId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}