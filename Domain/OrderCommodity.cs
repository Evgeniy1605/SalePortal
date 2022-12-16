using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class OrderCommodity : IOrderCommodity
    {
        private readonly IUserHttpClient _userHttpClient;
        private readonly ICommodityHttpClient _commodityHttpClient;
        private readonly IOrderHttpClient _orderHttp;
        public OrderCommodity(IUserHttpClient userHttpClient, ICommodityHttpClient commodityHttpClient, SalePortalDbConnection context, IOrderHttpClient orderHttp)
        {
            _userHttpClient = userHttpClient;
            _commodityHttpClient = commodityHttpClient;
            _orderHttp = orderHttp;
        }

        public async Task AddOrderAsync(int commodityId, int customerId)
        {

            CommodityOrderEntity order= new CommodityOrderEntity();
            var customer = await _userHttpClient.GetUserByIdAsync(customerId);
            order.CustomerId = customerId;
            var commodity = await _commodityHttpClient.GetCommodityByIdAsync(commodityId);
            order.CommodityOwnerId = commodity.OwnerId;
            order.CommodityId = commodityId;
            var owner = await _userHttpClient.GetUserByIdAsync(commodity.OwnerId);
            await _orderHttp.PostOrderAsync(order);

        }

        public async Task ApproveOrderAsync(int orderId)
        {

            var order = await _orderHttp.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.ApprovedByOwner = true;
                await _orderHttp.PutOrderAsync(orderId, order);
            }
        }

        public async Task<CommodityOrderEntity> GetOrderAsync(int orderId)
        {
            return await _orderHttp.GetOrderByIdAsync(orderId);
        }

        public async Task<List<CommodityOrderEntity>> GetOrdersAsync(int userId)
        {
            var orders = await _orderHttp.GetOrdersAsync();
            return orders.Where(x => x.CustomerId == userId).ToList();
        }

        public async Task<List<CommodityOrderEntity>> GetSalesAsync(int userId)
        {
            var orders = await _orderHttp.GetOrdersAsync();
            return orders.Where(x => x.CommodityOwnerId == userId).ToList();
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _orderHttp.GetOrderByIdAsync(orderId);
            if (order != null)
            { 
                await _orderHttp.DeleteOrderAsync(orderId);
            }
        }

        public async Task UnApproveOrderAsync(int orderId)
        {
            var order = await _orderHttp.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.ApprovedByOwner = false;
                _orderHttp.PutOrderAsync(orderId, order);
            }

        }
    }
}
