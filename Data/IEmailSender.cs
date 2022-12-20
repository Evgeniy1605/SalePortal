namespace SalePortal.Data
{
    public interface IEmailSender
    {
        public Task SendAsync(string userEmail, string message);
    }
}
