using System.Collections.Generic;
using Core.Entities;

namespace API.Dtos
{
    public class CommentOrderDto
    {
        public int OrderId { get; set; }
        public int HistoryId { get; set; }
        public string Comment { get; set; }
        public List<int> AcceptedItemsId { get; set; }
        public List<int> PendingItemsId { get; set; }
    }
}