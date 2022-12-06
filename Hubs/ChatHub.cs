using Microsoft.AspNetCore.SignalR;

namespace SalePortal.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, int userId, int chatId)
        {
            await Clients.Group(chatId.ToString()).SendAsync("Receive", message);
        }
    }
}
