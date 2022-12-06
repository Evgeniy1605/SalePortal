using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalePortal.Data;
using SalePortal.Domain;
using SalePortal.Entities;

namespace SalePortal.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChat _chat;
        private readonly ICommodityHttpClient _commodityHttpClient;
        public ChatController(IChat chat, ICommodityHttpClient commodityHttpClient)
        {

            _chat = chat;
            _commodityHttpClient = commodityHttpClient;
        }

        [Authorize]
        public async Task<IActionResult> Index(int commodityId, int customerId)
        {
            var commodity = await _commodityHttpClient.GetCommodityByIdAsync(commodityId);
            int sellerId = commodity.OwnerId;
            var chat = await _chat.GetChatByCustomerIdSellerIdAsync(customerId, sellerId);
            if (chat == null)
            {
                return RedirectToAction(nameof(CreateChat), new {commodityId, customerId});
            }

            var chatView = await _chat.GetChatViewModelAsync(chat.Id);

            return View(chatView);
        }

        [Authorize]
        public async Task <IActionResult> CreateChat(int commodityId, int customerId)
        {

            await _chat.CreateChatAsync(customerId, commodityId);
            
            return RedirectToAction(nameof(Index), new {commodityId, customerId});
        }

        [Authorize]
        public async Task<IActionResult> CustomerChats(int userId)
        {
            var chats = await _chat.GetCustomersChatsAsync(userId);
            return PartialView("_ChatsAsCustomer", chats);
        }
        [Authorize]
        public async Task<IActionResult> SellerChats(int userId)
        {
            var chats = await _chat.GetSellersChatsAsync(userId);
            return PartialView("_ChatsAsSeller", chats);
        }
    }
}
