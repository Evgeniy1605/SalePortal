using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class Chat : IChat
    {
        public Task AddMessageByIdAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task CreateChatAsync(int customerId, int commodityId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteChatAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatEntity> GetCatByCustomerIdSellerIdAsync(int customerId, int sellerId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatEntity> GetChatByIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageEntity>> GetMessagesByChatIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
