using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Entities
{
    public class ChatEntity
    {
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        public UserEntity? Customer { get; set; }

        [ForeignKey("SellerId")]
        public int? SellerId { get; set; }
        public UserEntity? Seller { get; set; }

        [ForeignKey("CommodityId")]
        public int? CommodityId { get; set; }
        public CommodityEntity? Commodity { get; set; }
    }
}
