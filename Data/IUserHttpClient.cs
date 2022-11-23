using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IUserHttpClient
    {
        
        public Task<UserEntity>  GetUserByIdAsync(int id);
        public List<UserEntity> GetUsers();
        public Task PostUserAsync(UserEntity user);
    }
}
