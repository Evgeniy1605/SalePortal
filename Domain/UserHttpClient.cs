using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Text;

namespace SalePortal.Domain;

public class UserHttpClient : IUserHttpClient, IAdmins
{

    private readonly IConfiguration _configuration;
    public UserHttpClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserEntity> GetUserByIdAsync(int id)
    {
        string Uri = _configuration.GetSection("ApiUri").Value + "User";
        string Key = _configuration.GetSection("ApiKey").Value;
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
        string Uri = _configuration.GetSection("ApiUri").Value + "User";
        string Key = _configuration.GetSection("ApiKey").Value;
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
    public async Task PostUserAsync(UserEntity user)
    {
        string Uri = _configuration.GetSection("ApiUri").Value + "User";
        string Key = _configuration.GetSection("ApiKey").Value;
        var postJson = JsonConvert.SerializeObject(user);
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

    public async Task<bool> DeleteUserAsync(int userId)
    {
        string Uri = _configuration.GetSection("ApiUri").Value + "User";
        string Key = _configuration.GetSection("ApiKey").Value;
        var client = new HttpClient();
        var uri = new Uri(Uri + "/" + userId.ToString());

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
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally { client.Dispose(); }
    }

    public async Task<bool> PutUserAsync(int userId, UserEntity user)
    {
        string Uri = _configuration.GetSection("ApiUri").Value + "User";
        string Key = _configuration.GetSection("ApiKey").Value;
        var postJson = JsonConvert.SerializeObject(user);
        var content = new StringContent(postJson, Encoding.UTF8, "application/json");

        var client = new HttpClient();
        var uri = new Uri(Uri + "/" + userId.ToString());
        try
        {
            var reqest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
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
            return true;

        }
        catch (Exception)
        {

            return false;
        }
        finally
        {
            client.Dispose();
        }
    }

    public Task<List<AdminEntity>> GetAdminsAsync()
    {
        throw new NotImplementedException();
    }
}


