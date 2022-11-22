using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain;

public class UserHttpClient : IUserHttpClient
{
    private const string Key = "pgHlpp7QzFasHJx4w46fI5Uzi4RvtTwlEXpsarwrsf8872dsd";
    private const string Uri = "https://localhost:7165/api/User";
    public async Task<UserEntity> GetUserByIdAsync(int id)
    {
        string json;
        var client = new HttpClient();
        UserEntity result;
        try
        {
            var reqest = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Uri + "/" + id.ToString()),
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
            result = JsonConvert.DeserializeObject<UserEntity>(json);
            return result;
        }
        catch (Exception)
        {
            result = new UserEntity();
            return result;
        }
        finally { client.Dispose(); };
    }


    public  List<UserEntity> GetUsers()
    {
        string json;
        var client = new HttpClient();
        List<UserEntity> result = new List<UserEntity>();
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

            using (var response = client.Send(reqest))
            {
                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;
                
            }

            result = JsonConvert.DeserializeObject<List<UserEntity>>(json);
        }
        catch (Exception)
        {

            
            return result;
        }
        finally
        {
            client.Dispose();
        }
        return result;
    }
}


