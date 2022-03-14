using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _orderRepo;
        private readonly IGenericRepository<KitOrderHistory> _historyRepo;
        private readonly IGenericRepository<KitOrderItem> _itemRepo;
        private readonly IGenericRepository<ItemHistory> _itemHistoryRepo;
         private readonly UserManager<FarmerUser> _userManager;
        public OrderService(IOrdersRepository orderRepo, IGenericRepository<KitOrderHistory> historyRepo, IGenericRepository<KitOrderItem> itemRepo, IGenericRepository<ItemHistory> itemHistoryRepo, UserManager<FarmerUser> userManager)
        {
            _itemRepo = itemRepo;
            _historyRepo = historyRepo;
            _orderRepo = orderRepo;
            _itemHistoryRepo = itemHistoryRepo;

             _userManager = userManager;
        }


        public async Task<KitOrder> CreateOrder(string farmerEmail, List<OrderItemDto> items)
        {

            decimal orderPoints = 0;
            var order = new KitOrder();
            var orderItems = new List<KitOrderItem>();
            try
            {
                foreach (var item in items)
                {
                    var itemOrderPoints = item.TypeValue * 24 * item.Acres;
                    orderPoints += itemOrderPoints;

                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, itemOrderPoints, item.Acres, false);
                    orderItems.Add(orderItem);
                }

                string status = "pending";
                var dateTimeNow = DateTime.Now;
                var histories = new List<KitOrderHistory>();
                var orderHistory = new KitOrderHistory(orderItems, dateTimeNow, orderPoints, "pending", farmerEmail);
                // var addressOrder = new AddressOrder(address.FirstName, address.LastName, address.Street, address.City, address.ZipCode);
                histories.Add(orderHistory);
                order = new KitOrder(histories, farmerEmail, dateTimeNow, status, orderPoints);

                _orderRepo.Add(order);

            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return order;

        }


        public async Task<KitOrder> UpdateOrder(int orderId, int historyId, List<KitOrderItem> items, string email)
        {

            decimal orderPoints = 0;
            KitOrder order;
            List<KitOrderItem> approvedItems = _orderRepo.ListItemsByHistory(historyId).Result.FindAll(x => x.Approved == true);
            var orderItems = new List<KitOrderItem>();
            try
            {
                order = _orderRepo.GetByIdAsync(orderId).Result;
                if (email != order.FarmerEmail)
                {

                }
                foreach (var item in items)
                {

                    var itemOrderPoints = item.TypeValue * 24 * item.Acres;
                    orderPoints += itemOrderPoints;
                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, itemOrderPoints, item.Acres, false);
                    orderItems.Add(orderItem);
                }

                foreach (var item in approvedItems)
                {

                    orderPoints += item.OrderItemPoints;
                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, item.OrderItemPoints, item.Acres, false);
                    orderItems.Add(orderItem);
                }


                var dateTimeNow = DateTime.Now;
                var orderHistory = new KitOrderHistory(orderItems, dateTimeNow, orderPoints, "farmerReviewed", email);
                order.OrderHistories.Add(orderHistory);
                _orderRepo.Update(order);


            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return order;
        }





        public async Task<KitOrder> AcceptOrder(int orderId, int historyId, string cooperativeEmail)
        {
            var order = _orderRepo.GetByIdAsync(orderId).Result;
            var history = _historyRepo.GetByIdAsync(historyId).Result;
            List<KitOrderItem> items;
            var newOrderItems = new List<KitOrderItem>();

            try
            {
                items = _orderRepo.ListItemsByHistory(historyId).Result;
                foreach (var item in items)
                {
                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, item.OrderItemPoints, item.Acres, true);
                    newOrderItems.Add(orderItem);
                }

                var dateTimeNow = DateTime.Now;

                order.Status = "approved";
                order.DatetimeOfOrderApproval = dateTimeNow;
                order.ApprovedBy = cooperativeEmail;

                var orderHistory = new KitOrderHistory(newOrderItems, dateTimeNow, history.OrderPoints, "approved", null, cooperativeEmail, history.FarmerEmail);
                order.OrderHistories.Add(orderHistory);


                _orderRepo.Update(order);
                //update user points
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == order.FarmerEmail);
                user.Points-=order.OrderPoints;
                var result = await _userManager.UpdateAsync(user);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return order;
        }



        public async Task<KitOrder> CommentOrder(int orderId, int historyId, string cooperativeEmail, string comment, List<int> approvedItemsId, List<int> pendingItemsId)
        {
            KitOrder order;
            var history = _historyRepo.GetByIdAsync(historyId).Result;
            var orderItems = new List<KitOrderItem>();

            try
            {
                order = _orderRepo.GetByIdAsync(orderId).Result;
                foreach (var itemId in approvedItemsId)
                {
                    var item = _itemRepo.GetByIdAsync(itemId).Result;

                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, item.OrderItemPoints, item.Acres, true);
                    orderItems.Add(orderItem);
                }

                foreach (var itemId in pendingItemsId)
                {
                    var item = _itemRepo.GetByIdAsync(itemId).Result;
                    var orderItem = new KitOrderItem(item.Type, item.TypeValue, item.OrderItemPoints, item.Acres, false);
                    orderItems.Add(orderItem);

                }

                var dateTimeNow = DateTime.Now;
                order.Status = "checked";
                order.DatetimeOfOrderApproval = dateTimeNow;

                var orderHistory = new KitOrderHistory(orderItems, dateTimeNow, history.OrderPoints, "coopReviewed", comment, cooperativeEmail, history.FarmerEmail);
                order.OrderHistories.Add(orderHistory);

                _orderRepo.Update(order);


            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            return order;
        }

        public Task<KitOrder> GetOrdersById(int id, string farmerEmail)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<KitOrder>> GetOrdersByUser(string farmerEmail)
        {
            var orders = new List<KitOrder>();
            try
            {
                orders = _orderRepo.ListOrdersByUser(farmerEmail).Result;
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return orders;
        }

        public async Task<KitOrder> GetOrderById(int orderId)
        {
            var order = new KitOrder();
            try
            {
                order = _orderRepo.GetOrderById(orderId).Result;
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return order;
        }

        public async Task<List<KitOrder>> GetAllOrders()
        {
            var orders = new List<KitOrder>();
            try
            {
                orders = _orderRepo.ListAllOrders().Result;
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return orders;
        }


        public async Task<KitOrderHistory> GetHistoryById(int id)
        {
            var history = new KitOrderHistory();
            try
            {
                history = _orderRepo.GetHistoryById(id).Result;
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return history;
        }
        //get historybyid
    }
}