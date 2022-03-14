using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrdersRepository: IGenericRepository<KitOrder>
    {
        Task<List<KitOrderItem>> ListItemsByHistory(int id);
        Task<IReadOnlyList<KitOrderHistory>> ListHistoriesByOrder(int id);

        Task<List<KitOrder>> ListOrdersByUser(string email);
        Task<List<KitOrder>> ListAllOrders();
        Task<KitOrder> GetOrderById(int id);

        Task<KitOrderHistory> GetHistoryById(int id);
        
    }
}