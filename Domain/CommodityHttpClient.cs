using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;

namespace SalePortal.Domain
{
    public class CommodityHttpClient : ICommodityHttpClient
    {
        public  async Task<List<CommodityEntity>> GetCommoditiesAsync()
        {
            string json;
            var client = new HttpClient();
            List<CommodityEntity> result;
            try
            {
                var reqest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://localhost:7165/api/Commodities"),
                    Headers =
                {
                    { "ApiKey", "pgHlpp7QzFasHJx4w46fI5Uzi4RvtTwlEXpsarwrsf8872dsd" }
                }
                };

                using (var response = await client.SendAsync(reqest))
                {
                    response.EnsureSuccessStatusCode();
                    json = await response.Content.ReadAsStringAsync();
                }

                result = JsonConvert.DeserializeObject<List<CommodityEntity>>(json);
            }
            catch (Exception)
            {

                result= new List<CommodityEntity>();
            }
            finally
            {
                client.Dispose();
            }
            
            return result;
        }
    }
}
