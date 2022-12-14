using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Text;

namespace SalePortal.Domain
{
    public class ChatHttpClient : IChatHttpClient, IMessageHttpClient
    {
        private readonly IConfiguration _configuration;
        public ChatHttpClient(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public async Task DeleteChatAsync(int chatId)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Chats";
            string Key = _configuration.GetSection("ApiKey").Value;
            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + chatId.ToString());

            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Delete,
                    RequestUri = uri,

                    Headers =
                {
                    {"ApiKey", Key }
                }
                };
                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();

                }

            }
            catch (Exception)
            {
                int x = 1;
            }
            finally { client.Dispose(); }
        }

        public async Task<ChatEntity> GetChatAsyncById(int chatId)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Chats";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            ChatEntity result;
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Uri + "/" + chatId.ToString()),
                    Headers =
                {
                    { "ApiKey", Key }
                }
                };

                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();
                }
                result = JsonConvert.DeserializeObject<ChatEntity>(json);
                return result;
            }
            catch (Exception)
            {
                result = new ChatEntity();
                return result;
            }
            finally { client.Dispose(); };
        }

        public async Task<List<ChatEntity>> GetChatsAsync()
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Chats";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            List<ChatEntity> result;
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Uri),
                    Headers =
                {
                    { "ApiKey", Key }
                }
                };

                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();
                }

                result = JsonConvert.DeserializeObject<List<ChatEntity>>(json);
            }
            catch (Exception)
            {

                result = new List<ChatEntity>();
            }
            finally
            {
                client.Dispose();
            }

            return result;
        }

        public Task<MessageEntity> GetMessageAsyncById(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MessageEntity>> GetMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task PostChatAsync(ChatEntity chat)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Chats";
            string Key = _configuration.GetSection("ApiKey").Value;
            var postJson = JsonConvert.SerializeObject(chat);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = new Uri(Uri);
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = content,
                    Headers =
                {
                    {"ApiKey", Key }
                }
                };
                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                }


            }
            catch (Exception)
            {

            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task PostMessageAsync(MessageEntity message)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Messagses";
            string Key = _configuration.GetSection("ApiKey").Value;
            var postJson = JsonConvert.SerializeObject(message);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = new Uri(Uri);
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Content = content,
                    Headers =
                {
                    {"ApiKey", Key }
                }
                };
                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();

                }
                

            }
            catch (Exception)
            {
                
            }
            finally
            {
                client.Dispose();
            }
        }
            
        

        public Task PutChatAsync(int cahtId, ChatEntity chat)
        {
            throw new NotImplementedException();
        }
    }
}
