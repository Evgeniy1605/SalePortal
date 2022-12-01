using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class OrderCommodity : IOrderCommodity
    {
        private readonly IUserHttpClient _userHttpClient;
        private readonly ICommodityHttpClient _commodityHttpClient;
        private readonly SalePortalDbConnection _context;
        public OrderCommodity(IUserHttpClient userHttpClient, ICommodityHttpClient commodityHttpClient, SalePortalDbConnection context)
        {
            _userHttpClient= userHttpClient;
            _commodityHttpClient= commodityHttpClient;
            _context= context;
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
            await _context.CommodityOrders.AddAsync(order);
            await _context.SaveChangesAsync();

        }

        public Task ApproveOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
