using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class OrderHttpClient : IOrderHttpClient
    {
        public Task DeleteOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<CommodityOrderEntity> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommodityOrderEntity>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task PostOrderAsync(CommodityOrderEntity order)
        {
            throw new NotImplementedException();
        }

        public Task PutOrderAsync(int orderId, CommodityOrderEntity order)
        {
            throw new NotImplementedException();
        }
    }
}
