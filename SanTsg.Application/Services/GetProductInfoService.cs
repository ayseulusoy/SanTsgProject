using Newtonsoft.Json;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SanTsgProject.Application.Models.GetProductInfoRequest;

namespace SanTsgProject.Application.Services
{
    public class GetProductInfoService : IGetProductInfoService
    {
        public async Task<GetProductInfoResponse.Hotel> GetDetails(string productId, string token)
        {
            var productRequest = new Root()
            {
                productType = 2,
                ownerProvider = 2,
                product = productId,
                culture = "en-US",

            };
            using (var client = new HttpClient())
            {
                string strProductRequest = JsonConvert.SerializeObject(productRequest);
                HttpContent httpContent = new StringContent(strProductRequest, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var Res = await client.PostAsync("http://service.stage.paximum.com/api/productservice/getproductinfo", httpContent);
                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync();
                    GetProductInfoResponse.Root model = JsonConvert.DeserializeObject<GetProductInfoResponse.Root>(res.Result);
                    GetProductInfoResponse.Hotel hotel = model.body.hotel;
                    return hotel;
                }
                return null;
            }
        }
    }
}
