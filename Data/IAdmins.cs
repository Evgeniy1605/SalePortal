using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IAdmins
    {
        public Task<List<AdminEntity>> GetAdminsAsync();
    }
}
