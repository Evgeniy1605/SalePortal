﻿using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IChat
    {
        public Task<ChatEntity> GetCatByCustomerIdSellerIdAsync(int customerId, int sellerId);
        public Task CreateChatAsync(int customerId, int commodityId);

        public Task<List<MessageEntity>> GetMessagesByChatIdAsync(int chatId);
        public Task DeleteChatAsync(int chatId);
        public Task DeleteMessageAsync(int messageId);
        public Task<ChatEntity> GetChatByIdAsync(int chatId);
        public Task AddMessageByIdAsync(int messageId);
    }
}
