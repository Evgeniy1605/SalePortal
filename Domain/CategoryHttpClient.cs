using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class CategoryHttpClient : ICategoryHttpClient
    {
        private const string Key = "pgHlpp7QzFasHJx4w46fI5Uzi4RvtTwlEXpsarwrsf8872dsd";
        private const string Uri = "https://localhost:7165/api/Categories";
        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryEntity>> GetCategoriesAsync()
        {
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

        public Task<CategoryEntity> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PostCategoryAsync(CategoryEntity category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutCategoryAsync(int categoryId, CategoryEntity category)
        {
            throw new NotImplementedException();
        }
    }
}
