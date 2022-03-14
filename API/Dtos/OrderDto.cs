using System.Collections.Generic;
using Core.Entities;

namespace API.Dtos
{
    public class OrderDto
    {
        public List<OrderItemDto> Items { get; set; }
        // public AddressDto ShipToAddress { get; set; }
    }
}