﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalePortal.Data;
using SalePortal.Entities;
using System.Text;

namespace SalePortal.Domain
{
    public class OrderHttpClient : IOrderHttpClient
    {
        private const string Key = "pgHlpp7QzFasHJx4w46fI5Uzi4RvtTwlEXpsarwrsf8872dsd";
        private const string Uri = "https://localhost:7165/api/Orders";
        public Task DeleteOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<CommodityOrderEntity> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommodityOrderEntity>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task PostOrderAsync(CommodityOrderEntity order)
        {
            
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

        public Task PutOrderAsync(int orderId, CommodityOrderEntity order)
        {
            throw new NotImplementedException();
        }
    }
}