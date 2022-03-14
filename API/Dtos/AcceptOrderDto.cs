using System.Collections.Generic;
using Core.Entities;

namespace API.Dtos
{
    public class AcceptOrderDto
    {
        public int OrderId { get; set; }
        public int HistoryId { get; set; }
    }
}