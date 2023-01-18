using SalePortal.Entities.NovaPoshtaEntities;

namespace SalePortal.Data
{
    public interface INovaPoshtaPostOffices
    {
        public ValueTask<List<Datum>> GetPostOfficesAsync(string cityName);
    }
}
