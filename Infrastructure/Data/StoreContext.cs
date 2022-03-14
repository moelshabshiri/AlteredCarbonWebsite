using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<KitOrder> KitOrders { get; set; }
        public DbSet<KitOrderHistory> KitOrderHistories { get; set; }
        public DbSet<KitOrderItem> KitOrderItems { get; set; }
        public DbSet<ItemHistory> itemHistorys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}