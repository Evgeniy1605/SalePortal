using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface ICommodityHttpClient
    {
        public Task<List<CommodityEntity>> GetCommoditiesAsync();
    }
}
