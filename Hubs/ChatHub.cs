using Microsoft.AspNetCore.SignalR;
using SalePortal.Data;

namespace SalePortal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChat _chat;
        private readonly IUserHttpClient _userHttp;
        public ChatHub(IChat chat, IUserHttpClient userHttp)
        {
            _chat = chat;
            _userHttp = userHttp;
        }

        public async Task Enter(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
            
        }

        public async Task Send(string message, int userId, int chatId)
        {
            await _chat.AddMessageAsync(chatId, userId, message);
            var user = await _userHttp.GetUserByIdAsync(userId);
            string userName = user.Name;
            string group = chatId.ToString();
            await Clients.Group(group).SendAsync("Receive", message, userName);
        }
    }
}
