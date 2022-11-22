using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IUserHttpClient
    {
        
        public Task<UserEntity>  GetUserByIdAsync(int id);
    }
}
