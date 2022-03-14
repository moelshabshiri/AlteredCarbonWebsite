using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrdersRepository : GenericRepository<KitOrder>, IOrdersRepository
    {
        private readonly StoreContext _context;
        public OrdersRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<KitOrderItem>> ListItemsByHistory(int id)
        {
            var orderHistory = await _context.KitOrderHistories.Include(x=>x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

            return orderHistory.OrderItems.ToList();
        }

        public async Task<IReadOnlyList<KitOrderHistory>> ListHistoriesByOrder(int id)
        {
            throw new System.NotImplementedException();
        }


         public async Task<List<KitOrder>> ListOrdersByUser(string email)
        {
            var orders = await _context.KitOrders.Where(x => x.FarmerEmail == email).ToListAsync();
            return orders;
        }

        public async Task<KitOrder> GetOrderById(int id)
        {
            var order = await _context.KitOrders.Include(x=>x.OrderHistories).FirstOrDefaultAsync(x => x.Id == id);

            return order;
        }

         public async Task<KitOrderHistory> GetHistoryById(int id)
        {
            var order = await _context.KitOrderHistories.Include(x=>x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

            return order;
        }

        public async Task<KitOrder> GetItemByHistory(int id)
        {
            var order = await _context.KitOrders.Include(x=>x.OrderHistories).ThenInclude(post => post.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

            return order;
        }

        public async Task<List<KitOrder>> ListAllOrders()
        {
            var orders = await _context.KitOrders.ToListAsync();
            return orders;
        }


        // public async Task<List<KitOrder>> GetApprovedItemsByOrder(int id)
        // {
        //     var order = await _context.KitOrders.Include(x=>x.OrderHistories).ThenInclude(post => post.OrderItems).FirstOrDefaultAsync(x => x.Id == id);

        //     return order;
        // }
    }
}