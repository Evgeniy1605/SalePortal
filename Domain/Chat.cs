using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Entities;
using SalePortal.Models;

namespace SalePortal.Domain
{
    public class Chat : IChat
    {
        private readonly SalePortalDbConnection _context;
        private readonly ICommodityHttpClient _httpClientCommodity;
        private readonly IUserHttpClient _userHttp;
        private readonly IMessageHttpClient _messageHttp;
        private readonly IChatHttpClient _chatHttp;
        public Chat(SalePortalDbConnection context, ICommodityHttpClient httpClientCommodity, IUserHttpClient userHttp, IMessageHttpClient messageHttp, IChatHttpClient chatHttp)
        {
            _context = context;
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
            return await _context.Chats.SingleOrDefaultAsync(x => x.CustomerId == customerId && x.SellerId == sellerId);
        }

        public async Task<ChatEntity> GetChatByIdAsync(int chatId)
        {
            return await _context.Chats.SingleOrDefaultAsync(x => x.Id == chatId);
        }

        public async Task<ChatViewModel> GetChatViewModelAsync(int chatId)
        {
            ChatViewModel chatView = new ChatViewModel();
            var chat = await _context.Chats.Include(x =>x.Commodity).Include(x => x.Seller).SingleOrDefaultAsync(x => x.Id == chatId);
            if (chat == null)
            {
                return chatView;
            }
            var messeges = await _context.Messages.Include(x => x.Sender).Where(x => x.ChatId == chatId).ToListAsync();
            chatView.Chat = chat;
            chatView.Messages = messeges;
            return chatView;
        }

        public async Task<List<ChatEntity>> GetCustomersChatsAsync(int userId)
        {
            return await _context.Chats
                .Include(x => x.Commodity)
                .Where(x => x.CustomerId == userId)
                .ToListAsync();
        }

        public Task<List<MessageEntity>> GetMessagesByChatIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatEntity>> GetSellersChatsAsync(int userId)
        {
            return await _context.Chats
                .Include(x => x.Commodity)
                .Where(x => x.SellerId == userId)
                .ToListAsync();
        }
    }
}
