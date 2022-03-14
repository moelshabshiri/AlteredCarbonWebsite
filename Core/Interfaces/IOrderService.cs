using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<KitOrder> CreateOrder(string farmerEmail, List<OrderItemDto> items);
        Task<KitOrder> UpdateOrder(int orderId, int historyId, List<KitOrderItem> Items, string email);
        Task<KitOrder> AcceptOrder(int orderId, int historyId, string cooperativeEmail);
        Task<KitOrder> CommentOrder(int orderId, int historyId, string cooperativeEmail, string comment, List<int> approvedItemsId, List<int> pendingItemsId);


        Task<KitOrder> GetOrderById(int orderId);

        Task<List<KitOrder>> GetOrdersByUser(string farmerEmail);
        Task<List<KitOrder>> GetAllOrders();
        Task<KitOrder> GetOrdersById(int id, string farmerEmail);

        Task<KitOrderHistory> GetHistoryById(int id);
    }
}