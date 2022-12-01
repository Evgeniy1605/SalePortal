namespace SalePortal.Data
{
    public interface IOrderCommodity
    {
        public Task AddOrderAsync(int commodityId, int customerId);
        public Task RemoveOrderAsync(int orderId);
        public Task ApproveOrderAsync(int orderId);
    }
}
