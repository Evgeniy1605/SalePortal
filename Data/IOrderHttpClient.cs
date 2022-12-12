using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IOrderHttpClient
    {
        public Task<List<CommodityOrderEntity>> GetOrdersAsync();
        public Task<CommodityOrderEntity> GetOrderByIdAsync(int id);
        public Task PostOrderAsync(CommodityOrderEntity order);
        public Task DeleteOrderAsync(int orderId);
        public Task PutOrderAsync(int orderId , CommodityOrderEntity order);
    }
}
