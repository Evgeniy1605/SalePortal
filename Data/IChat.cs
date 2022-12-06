using SalePortal.Entities;
using SalePortal.Models;

namespace SalePortal.Data
{
    public interface IChat
    {
        public Task<ChatEntity> GetChatByCustomerIdSellerIdAsync(int customerId, int sellerId);
        public Task CreateChatAsync(int customerId, int commodityId);

        public Task<List<MessageEntity>> GetMessagesByChatIdAsync(int chatId);
        public Task DeleteChatAsync(int chatId);
        public Task DeleteMessageAsync(int messageId);
        public Task<ChatEntity> GetChatByIdAsync(int chatId);
        public Task AddMessageAsync(int chatId, int senderId, string message);

        public Task<ChatViewModel> GetChatViewModelAsync(int chatId);



    }
}
