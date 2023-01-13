using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Security.Claims;
using System.Text;

namespace SalePortal.Domain
{
    public class CategoryHttpClient : ICategoryHttpClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public CategoryHttpClient(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            string Token = _contextAccessor.HttpContext.User.FindFirstValue("Token");
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + categoryId.ToString());

            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Delete,
                    RequestUri = uri,

                    Headers =
                {
                    {"ApiKey", Key },
                    { "Authorization", $"Bearer {Token}"}
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

        public List<CategoryEntity> GetCategories()
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            List<CategoryEntity> result = new List<CategoryEntity>();
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

                result = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);
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

        public async Task<List<CategoryEntity>> GetCategoriesAsync()
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            List<CategoryEntity> result  = new List<CategoryEntity>();
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

                result = JsonConvert.DeserializeObject<List<CategoryEntity>>(json);
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

        public async Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            CategoryEntity result;
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
                result = JsonConvert.DeserializeObject<CategoryEntity>(json);
                return result;
            }
            catch (Exception)
            {
                result = new CategoryEntity();
                return result;
            }
            finally { client.Dispose(); };
        }

        public async Task<bool> PostCategoryAsync(CategoryEntity category)
        {
            string Token = _contextAccessor.HttpContext.User.FindFirstValue("Token");
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            var postJson = JsonConvert.SerializeObject(category);
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
                        {"ApiKey", Key },
                        { "Authorization", $"Bearer {Token}"}
                    },
                    
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

        public async Task<bool> PutCategoryAsync(int categoryId, CategoryEntity category)
        {
            string Token = _contextAccessor.HttpContext.User.FindFirstValue("Token");
            string Uri = _configuration.GetSection("ApiUri").Value + "Categories";
            string Key = _configuration.GetSection("ApiKey").Value;
            var postJson = JsonConvert.SerializeObject(category);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + categoryId.ToString());
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = uri,
                    Content = content,
                    Headers =
                {
                    {"ApiKey", Key },
                    { "Authorization", $"Bearer {Token}"}
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

       
    }
}
