using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IMessageHttpClient
    {
        public Task<List<MessageEntity>> GetMessagesAsync();
        public Task<MessageEntity> GetMessageAsyncById(int messageId);
        public Task PostMessageAsync(MessageEntity message);
    }
}
