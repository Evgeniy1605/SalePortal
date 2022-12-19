namespace SalePortal.Data
{
    public interface IEmailSender
    {
        public Task Send(string userEmail, string message);
    }
}
