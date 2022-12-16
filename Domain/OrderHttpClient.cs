using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Text;

namespace SalePortal.Domain
{
    public class OrderHttpClient : IOrderHttpClient
    {
        private readonly IConfiguration _configuration;
        public OrderHttpClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Orders";
            string Key = _configuration.GetSection("ApiKey").Value;
            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + orderId.ToString());

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
                
            }
            finally { client.Dispose(); }
        }

        public async Task<CommodityOrderEntity> GetOrderByIdAsync(int id)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Orders";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            CommodityOrderEntity result;
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
                result = JsonConvert.DeserializeObject<CommodityOrderEntity>(json);
                return result;
            }
            catch (Exception)
            {
                result = new CommodityOrderEntity();
                return result;
            }
            finally { client.Dispose(); };
        }

        public async Task<List<CommodityOrderEntity>> GetOrdersAsync()
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Orders";
            string Key = _configuration.GetSection("ApiKey").Value;
            string json;
            var client = new HttpClient();
            List<CommodityOrderEntity> result ;
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

                result = JsonConvert.DeserializeObject<List<CommodityOrderEntity>>(json);
            }
            catch (Exception)
            {

                result = new List<CommodityOrderEntity>();
            }
            finally
            {
                client.Dispose();
            }

            return result;
        }

        public async Task PostOrderAsync(CommodityOrderEntity order)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Orders";
            string Key = _configuration.GetSection("ApiKey").Value;
            var postJson = JsonConvert.SerializeObject(order);
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

        public async Task PutOrderAsync(int orderId, CommodityOrderEntity order)
        {
            string Uri = _configuration.GetSection("ApiUri").Value + "Orders";
            string Key = _configuration.GetSection("ApiKey").Value;
            order.Commodity = null;
            order.CommodityOwner= null;
            order.Customer = null;

            //
            var postJson = JsonConvert.SerializeObject(order);
            var content = new StringContent(postJson, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = new Uri(Uri + "/" + orderId.ToString());
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

            }
            catch (Exception )
            {

            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
