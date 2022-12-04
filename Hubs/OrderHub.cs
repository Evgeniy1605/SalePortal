using Microsoft.AspNetCore.SignalR;
using SalePortal.Data;

namespace SalePortal.Hubs
{
    public class OrderHub : Hub
    {
        private readonly IOrderCommodity _orderCommodity;
        public OrderHub(IOrderCommodity orderCommodity)
        {
            _orderCommodity= orderCommodity;
        }
        public async Task Orders(int userId)
        {
            var result = await _orderCommodity.GetOrdersAsync(userId);
            await this.Clients.Caller.SendAsync("Receive", result);
        }

        public async Task Sales(int userId)
        {
            var result = await _orderCommodity.GetSalesAsync(userId);
            await this.Clients.Caller.SendAsync("Receive", result);
        }
    }
}
