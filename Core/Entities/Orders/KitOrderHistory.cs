using System;
using System.Collections.Generic;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class KitOrderHistory : BaseEntity
    {
        public KitOrderHistory()
        {
        }
        public KitOrderHistory(List<KitOrderItem> orderItems, DateTime datetimeOfHistory, decimal orderPoints, string status, string farmerEmail
            )
        {
            OrderItems = orderItems;
            DatetimeOfHistory = datetimeOfHistory;
            OrderPoints = orderPoints;
            Status = status;
            FarmerEmail = farmerEmail;
        }

        public KitOrderHistory(List<KitOrderItem> orderItems, DateTime datetimeOfHistory, decimal orderPoints, string status, string comment, string cooperativeUserEmail
           ,string farmerEmail)
        {
            OrderItems = orderItems;
            DatetimeOfHistory = datetimeOfHistory;
            OrderPoints = orderPoints;
            Status = status;
            Comment = comment;
            CooperativeUserEmail = cooperativeUserEmail;
            FarmerEmail = farmerEmail;
        }
        public DateTime DatetimeOfHistory { get; set; }
        public List<KitOrderItem> OrderItems { get; set; }
        // public List<ItemHistory> ItemHistorys { get; set; }
        public string Comment { get; set; }
        public string CooperativeUserEmail { get; set; }

        public decimal OrderPoints { get; set; }
        public string Status { get; set; }
        public string FarmerEmail { get; set; }
    }
}