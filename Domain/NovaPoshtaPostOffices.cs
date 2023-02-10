using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities.NovaPoshtaEntities;
using System.Security.Claims;

namespace SalePortal.Domain
{
    public class NovaPoshtaPostOffices : INovaPoshtaPostOffices
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public NovaPoshtaPostOffices(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public async ValueTask<List<Datum>> GetPostOfficesAsync(string cityName)
        {
            string Token = _contextAccessor.HttpContext.User.FindFirstValue("Token");
            string Uri = $"{_configuration.GetSection("ApiUri").Value}NovaPoshta?cityName={cityName}";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            List<Datum> result = new List<Datum>();
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(Uri),
                    Headers =
                    {
                        { "ApiKey", Key },
                        { "Authorization", $"Bearer {Token}"}
                    }
                };

                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();
                }

                result = JsonConvert.DeserializeObject<List<Datum>>(json);
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
}
