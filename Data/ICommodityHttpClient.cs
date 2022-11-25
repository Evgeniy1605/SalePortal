using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface ICommodityHttpClient
    {
        public Task<List<CommodityEntity>> GetCommoditiesAsync();
        public Task<CommodityEntity> GetCommodityByIdAsync(int? id);
        public Task<bool> PostCommoditiesAsync(CommodityEntity commodity, int userId);
        public Task<bool> DeleteCommodityAsync(int CommodityId);
        public Task<bool> PutCommodityAsync(int commodityId, int userId, CommodityEntity commodity);
    }
}
