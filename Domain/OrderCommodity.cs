using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class OrderCommodity : IOrderCommodity
    {
        private readonly IUserHttpClient _userHttpClient;
        private readonly ICommodityHttpClient _commodityHttpClient;
        private readonly SalePortalDbConnection _context;
        private readonly IOrderHttpClient _orderHttp;
        public OrderCommodity(IUserHttpClient userHttpClient, ICommodityHttpClient commodityHttpClient, SalePortalDbConnection context, IOrderHttpClient orderHttp)
        {
            _userHttpClient = userHttpClient;
            _commodityHttpClient = commodityHttpClient;
            _context = context;
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

            //var order = await _context.CommodityOrders.SingleOrDefaultAsync(x => x.Id == orderId);
            var order = await _orderHttp.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                order.ApprovedByOwner = true;
                _context.CommodityOrders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CommodityOrderEntity> GetOrderAsync(int orderId)
        {
            return await _context.CommodityOrders.Include(x => x.Commodity).Include(x => x.Customer).Include(x => x.CommodityOwner).SingleOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<List<CommodityOrderEntity>> GetOrdersAsync(int userId)
        {
            return await _context.CommodityOrders.Include(x =>x.Commodity).Include(x => x.Customer).Include(x => x.CommodityOwner).Where(x => x.CustomerId == userId).ToListAsync();
        }

        public async Task<List<CommodityOrderEntity>> GetSalesAsync(int userId)
        {
            return await _context.CommodityOrders.Include(x => x.Commodity).Include(x => x.Customer).Include(x => x.CommodityOwner).Where(x => x.CommodityOwnerId == userId).ToListAsync();
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _context.CommodityOrders.SingleOrDefaultAsync(x =>x.Id == orderId);
            if (order != null)
            {
                _context.CommodityOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnApproveOrderAsync(int orderId)
        {
            var order = await _context.CommodityOrders.SingleOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
            {
                order.ApprovedByOwner = false;
                _context.CommodityOrders.Update(order);
                await _context.SaveChangesAsync();
            }

        }
    }
}
