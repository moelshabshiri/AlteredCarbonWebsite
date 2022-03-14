using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class KitOrderItem : BaseEntity
    {

        public KitOrderItem()
        {
        }
        public KitOrderItem(string type, decimal typeValue, decimal orderItemPoints, decimal acres,
          Boolean approved  )
        {
            Type = type;
            TypeValue = typeValue;
            OrderItemPoints = orderItemPoints;
            Acres = acres;
            Approved = approved;
        }
        public string Type { get; set; }
        public decimal TypeValue { get; set; }
        public decimal OrderItemPoints { get; set; }
        public decimal Acres { get; set; }
        public Boolean Approved { get; set; }

        // public List<ItemHistory> ItemHistorys { get; set; }
    
        
    }
}