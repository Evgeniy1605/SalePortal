using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Entities;
using SalePortal.Models;
using System.Linq;

namespace SalePortal.Domain
{
    public class Chat : IChat
    {

        private readonly ICommodityHttpClient _httpClientCommodity;
        private readonly IUserHttpClient _userHttp;
        private readonly IMessageHttpClient _messageHttp;
        private readonly IChatHttpClient _chatHttp;
        public Chat(ICommodityHttpClient httpClientCommodity, IUserHttpClient userHttp, IMessageHttpClient messageHttp, IChatHttpClient chatHttp)
        {
            _httpClientCommodity = httpClientCommodity;
            _userHttp = userHttp;
            _messageHttp = messageHttp;
            _chatHttp = chatHttp;
        }

        public async Task AddMessageAsync(int chatId, int senderId, string message)
        {
            MessageEntity messageEntity = new MessageEntity()
            {
                ChatId = chatId,
                SenderId = senderId,
                Message = message,
                Date = DateTime.Now
            };
            await _messageHttp.PostMessageAsync(messageEntity);
        }

        public async Task CreateChatAsync(int customerId, int commodityId)
        {
            var commoduty = await _httpClientCommodity.GetCommodityByIdAsync(commodityId);
            var custumer = await _userHttp.GetUserByIdAsync(customerId);
            var seller = commoduty.Owner;
            ChatEntity chat = new ChatEntity() 
            { 
                CustomerId = customerId,
                SellerId = seller.Id,
                CommodityId = commodityId
            };
            
            await _chatHttp.PostChatAsync(chat);
        }

        public async Task DeleteChatAsync(int chatId)
        {
            var chat = await _chatHttp.GetChatAsyncById(chatId);
            
            if (chat != null)
            {
                await _chatHttp.DeleteChatAsync(chatId);
            }

        }

        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatEntity> GetChatByCustomerIdSellerIdAsync(int customerId, int sellerId)
        {
            var chats = await _chatHttp.GetChatsAsync();
            return chats.SingleOrDefault(x => x.CustomerId == customerId && x.SellerId == sellerId);
        }

        public async Task<ChatEntity> GetChatByIdAsync(int chatId)
        {

            return await _chatHttp.GetChatAsyncById(chatId);
        }

        public async Task<ChatViewModel> GetChatViewModelAsync(int chatId)
        {
            ChatViewModel chatView = new ChatViewModel();
            var chat = await _chatHttp.GetChatAsyncById(chatId);
            if (chat == null)
            {
                return chatView;
            }

            var messeges = await _messageHttp.GetMessagesAsync();
            chatView.Chat = chat;
            chatView.Messages = messeges.Where(x => x.ChatId == chatId).ToList();
            return chatView;
        }

        public async Task<List<ChatEntity>> GetCustomersChatsAsync(int userId)
        {
            var chats = await _chatHttp.GetChatsAsync();
            return chats
                .Where(x => x.CustomerId == userId)
                .ToList();
        }

        public Task<List<MessageEntity>> GetMessagesByChatIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatEntity>> GetSellersChatsAsync(int userId)
        {
            var chats = await _chatHttp.GetChatsAsync();
            return chats
                .Where(x => x.SellerId == userId)
                .ToList();
        }
    }
}
