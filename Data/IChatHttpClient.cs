using SalePortal.Entities;

namespace SalePortal.Data
{
    public interface IChatHttpClient
    {
        public Task<List<ChatEntity>> GetChatsAsync();
        public Task<ChatEntity> GetChatAsyncById(int chatId);
        public Task PostChatAsync(ChatEntity chat);
        public Task PutChatAsync(int cahtId, ChatEntity chat);
        public Task DeleteChatAsync(int chatId);

    }
}
