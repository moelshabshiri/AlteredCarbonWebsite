using System;
using System.Collections.Generic;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class KitOrder : BaseEntity
    {
        public KitOrder()
        {
        }
        public KitOrder(List<KitOrderHistory> orderHistories, string farmerEmail, DateTime datetimeOfOrderCreation,
            string status, decimal orderPoints
            )
        {
            FarmerEmail = farmerEmail;
            OrderHistories = orderHistories;
            DatetimeOfOrderCreation = datetimeOfOrderCreation;
            Status = status;
            OrderPoints = orderPoints;
            // ShipToAddress=shipToAddress;
            
        }
        public string FarmerEmail { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DatetimeOfOrderCreation { get; set; }
        public DateTime DatetimeOfOrderApproval { get; set; }
        public List<KitOrderHistory> OrderHistories { get; set; }
        public string Status { get; set; }
        public decimal OrderPoints { get; set; }


    }
}