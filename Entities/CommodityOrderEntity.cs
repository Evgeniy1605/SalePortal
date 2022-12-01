using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Entities
{
    public class CommodityOrderEntity
    {
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        public UserEntity? Customer { get; set; }

        [ForeignKey("CommodityOwnerId")]
        public int? CommodityOwnerId { get; set; }
        public UserEntity? CommodityOwner { get; set; }

        [ForeignKey("CommodityId")]
        public int? CommodityId { get; set; }
        public CommodityEntity? Commodity { get; set; }

        public bool ApprovedByOwner { get; set; } = false;

    }
}
