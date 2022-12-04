using System.ComponentModel.DataAnnotations.Schema;

namespace SalePortal.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }

        [ForeignKey("ChatId")]
        public int? ChatId { get; set; }
        public ChatEntity? Chat { get; set; }

        [ForeignKey("SenderId")]
        public int? SenderId { get; set; }
        public UserEntity? Sender { get; set; }
        public string? Message { get; set; }
        public DateTime? Date  { get; set; }
    }
}
