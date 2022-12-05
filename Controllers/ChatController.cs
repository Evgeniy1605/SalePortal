using Microsoft.AspNetCore.Mvc;
using SalePortal.Data;

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

        public async Task<IActionResult> Index(int commodityId, int customerId)
        {
            var x = 1;
            var commodity = await _commodityHttpClient.GetCommodityByIdAsync(commodityId);
            int sellerId = commodity.OwnerId;
            var chat = _chat.GetCatByCustomerIdSellerIdAsync(customerId, sellerId);
            if (chat == null)
            {
                return RedirectToAction(nameof(CreateChat));
            }

            var messeges = await _chat.GetMessagesByChatIdAsync(chat.Id);

            return View(messeges);
        }

        public IActionResult CreateChat()
        {
            return View();
        }
    }
}
