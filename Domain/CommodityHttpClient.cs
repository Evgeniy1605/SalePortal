using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Text;

namespace SalePortal.Domain
{
    public class CommodityHttpClient : ICommodityHttpClient
    {
        private const string Key = "pgHlpp7QzFasHJx4w46fI5Uzi4RvtTwlEXpsarwrsf8872dsd";
        private readonly SalePortalDbConnection _context;
        private const string Uri = "https://localhost:7165/api/Commodities";
        private readonly IUserHttpClient _userHttp;
        public CommodityHttpClient(SalePortalDbConnection context, IUserHttpClient userHttp)
        {
            _context = context;
            _userHttp = userHttp;
        }
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

        public async Task<bool> PostCommoditiesAsync(CommodityEntity commodity, int userId)
        {
            
            var owner = await _userHttp.GetUserByIdAsync(userId);
            var type = await _context.Categories.SingleOrDefaultAsync(x => x.Id == commodity.TypeId);

            if (owner == null || type == null )
            {
                return false;
            }

            commodity.Owner = owner;
            commodity.Type = type;
            var postJson = JsonConvert.SerializeObject(commodity);
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

        public async Task<bool> DeleteCommodityAsync(int CommodityId)
        {
            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + CommodityId.ToString());

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

        public async Task<CommodityEntity> GetCommodityByIdAsync(int? id)
        {
            string json;
            var client = new HttpClient();
            CommodityEntity result;
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
                result = JsonConvert.DeserializeObject<CommodityEntity>(json);
                return result;
            }
            catch (Exception)
            {
                result = new CommodityEntity();
                return result;
            }
            finally { client.Dispose(); };
        }

        public async Task<bool> PutCommodityAsync(int commodityId, int userId, CommodityEntity commodity)
        {
            var owner = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            var type = await _context.Categories.SingleOrDefaultAsync(x => x.Id == commodity.TypeId);

            if (owner == null || type == null)
            {
                return false;
            }

            commodity.Owner = owner;
            commodity.Type = type;
            var postJson = JsonConvert.SerializeObject(commodity);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + commodityId.ToString());
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
            throw new NotImplementedException();
        }
    }
}

