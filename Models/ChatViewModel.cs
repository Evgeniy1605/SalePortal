using SalePortal.Entities;

namespace SalePortal.Models
{
    public class ChatViewModel
    {
        public ChatEntity Chat { get; set; }
        public List<MessageEntity> Messages { get; set; }
    }
}
