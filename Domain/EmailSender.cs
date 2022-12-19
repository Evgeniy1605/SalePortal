using SalePortal.Data;

namespace SalePortal.Domain
{
    public class EmailSender : IEmailSender
    {
        public Task Send(string userEmail, string message)
        {
            throw new NotImplementedException();
        }
    }
}
