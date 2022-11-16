using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Entities
{
    public class CommodityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }


        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public CategoryEntity Type { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
