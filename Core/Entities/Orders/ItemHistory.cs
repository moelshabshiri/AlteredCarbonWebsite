namespace Core.Entities
{
    public class ItemHistory : BaseEntity
    {

        public ItemHistory()
        { 
        }

        public ItemHistory(int historyId, KitOrderHistory history, int itemId,  KitOrderItem item )
        { 
            HistoryId=historyId;
            History=history;
            ItemId=itemId;
            Item=item;
        }
      
        public int HistoryId { get; set; }
        public KitOrderHistory History { get; set; }

        public int ItemId { get; set; }
        public KitOrderItem Item { get; set; }

       
    }
}