using Newtonsoft.Json;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SanTsgProject.Application.Services.PriceSearchService.loginmodel;

namespace SanTsgProject.Application.Services
{
    public class PriceSearchService : IPriceSearchService
    {
        public async Task<List<Hotels>> GetHotels(string cityId, string token)
        {
            List<Hotels> otel = new List<Hotels>();
            var modellogin = new Rootobject()
            {
                checkAllotment = true,
                checkStopSale = true,
                getOnlyDiscountedPrice = false,
                getOnlyBestOffers = true,
                productType = 2,
                arrivalLocations = new Arrivallocation[] { new Arrivallocation() { id = cityId } },
                roomCriteria = new Roomcriteria[] { new Roomcriteria() { adult = 1, childAges = new int[] { 2 } } },
                nationality = "DE",
                checkIn = "2023-06-20",
                night = 7,
                currency = "EUR",
                culture = "en-US"
            };

            var client = new HttpClient();
            string strhotelsRequest = JsonConvert.SerializeObject(modellogin);
            HttpContent httpContent = new StringContent(strhotelsRequest, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Res = await client.PostAsync("http://service.stage.paximum.com/api/productservice/pricesearch", httpContent);
            if (Res.IsSuccessStatusCode)
            {
                var res = Res.Content.ReadAsStringAsync();
                PriceSearchResponse.Root model = JsonConvert.DeserializeObject<PriceSearchResponse.Root>(res.Result);
                foreach (var item in model.body.hotels)
                {
                    foreach (var item2 in item.offers)
                    {
                        otel.Add(new Hotels(hotelName: item.name, id: item.id, stars: item.stars, rating: item.rating, price: item2.price.amount,offerId:item2.offerId));
                        
                    }

                }

                return otel;
            }
            return null;



        }
        public class loginmodel
        {
            public class Rootobject
            {
                public bool checkAllotment { get; set; }
                public bool checkStopSale { get; set; }
                public bool getOnlyDiscountedPrice { get; set; }
                public bool getOnlyBestOffers { get; set; }
                public int productType { get; set; }
                public Arrivallocation[] arrivalLocations { get; set; }
                public Roomcriteria[] roomCriteria { get; set; }
                public string nationality { get; set; }
                public string checkIn { get; set; }
                public int night { get; set; }
                public string currency { get; set; }
                public string culture { get; set; }
            }

            public class Arrivallocation
            {
                public string id { get; set; }
                public int type { get; set; } = 2;
            }

            public class Roomcriteria
            {
                public int adult { get; set; }
                public int[] childAges { get; set; }
            }

        }
    }
}
