using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Models
{
    public class CommodityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public UserModel Owner { get; set; }

        
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public Category Type { get; set; }
        public string Image { get; set; }
    }
}
