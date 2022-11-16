using SalePortal.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Models
{
    public class CommodityInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("TypeId")]
        public int TypeId { get; set; }
        public CategoryEntity Type { get; set; }
        public decimal Price { get; set; }
    }
}
