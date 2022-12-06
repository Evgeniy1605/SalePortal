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
        public Chat(SalePortalDbConnection context, ICommodityHttpClient httpClientCommodity, IUserHttpClient userHttp)
        {
            _context = context;
            _httpClientCommodity = httpClientCommodity;
            _userHttp = userHttp;
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
            await _context.Messages.AddAsync(messageEntity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateChatAsync(int customerId, int commodityId)
        {
            var commoduty = await _httpClientCommodity.GetCommodityByIdAsync(commodityId);
            var custumer = await _userHttp.GetUserByIdAsync(customerId);
            var seller = commoduty.Owner;
            ChatEntity chat = new ChatEntity() 
            { 
                CustomerId = customerId,
                //Customer = custumer,
                SellerId = seller.Id,
                //Seller = seller,
                CommodityId = commodityId//,
                //Commodity = commoduty
            };
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public Task DeleteChatAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatEntity> GetChatByCustomerIdSellerIdAsync(int customerId, int sellerId)
        {
            return await _context.Chats.SingleOrDefaultAsync(x => x.CustomerId == customerId && x.SellerId == sellerId);
        }

        public Task<ChatEntity> GetChatByIdAsync(int chatId)
        {
            throw new NotImplementedException();
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
    }
}
