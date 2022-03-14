using System.Collections.Generic;
using Core.Entities;

namespace API.Dtos
{
    public class UpdateOrderDto
    {
        public int OrderId { get; set; }
        public int HistoryId { get; set; }
        public List<KitOrderItem> Items { get; set; }
    }
}