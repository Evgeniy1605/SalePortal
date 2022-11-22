using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface ICommodityHttpClient
    {
        public Task<List<CommodityEntity>> GetCommoditiesAsync();
        public Task<bool> PostCommoditiesAsync(CommodityEntity commodity, int userId);
    }
}
