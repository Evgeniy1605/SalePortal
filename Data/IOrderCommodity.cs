using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IOrderCommodity
    {
        public Task AddOrderAsync(int commodityId, int customerId);
        public Task RemoveOrderAsync(int orderId);
        public Task ApproveOrderAsync(int orderId);
        public Task UnApproveOrderAsync(int orderId);
        public Task<List<CommodityOrderEntity>> GetOrdersAsync(int userId);
        public Task<List<CommodityOrderEntity>> GetSalesAsync(int userId);

        public Task<CommodityOrderEntity> GetOrderAsync(int orderId);
    }
}
