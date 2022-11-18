using SalePortal.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Models
{
    public class CommodityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        public UserEntity Owner { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
