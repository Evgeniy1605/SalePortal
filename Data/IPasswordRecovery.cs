namespace SalePortal.Data
{
    public interface IPasswordRecovery
    {
        public Task ChangePasswordAsync(int userId, string newPassword);
    }
}
