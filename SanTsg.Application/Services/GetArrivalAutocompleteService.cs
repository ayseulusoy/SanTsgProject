using Newtonsoft.Json;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Services
{
    public class GetArrivalAutocompleteService : IGetArrivalAutocompleteService
    {
        public async Task<string> GetCityId(GetArrivalAutocompleteRequest.Root cityIdRequest, string cityName, string token)
        {
            using (var client = new HttpClient())
            {
                string cityId;
                string city = cityName.ToString().ToLower();

                //şehir adının düzgün girilmesi için yaptım.
                city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city);

                //apiddeki ingilizce karakter içeren şehir isimleri için yaptım
                string source = "ığüşöçĞÜŞİÖÇ";
                string destination = "igusocGUSIOC";

                string enCity = city;

                for (int i = 0; i < source.Length; i++)
                {
                    enCity = enCity.Replace(source[i], destination[i]);
                }

                string strcityIdRequest = JsonConvert.SerializeObject(cityIdRequest);
                HttpContent httpContent = new StringContent(strcityIdRequest, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var Res = await client.PostAsync("http://service.stage.paximum.com/v2/api/productservice/getarrivalautocomplete", httpContent);
                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync();
                    GetArrivalAutocompleteResponse.Root model2 = JsonConvert.DeserializeObject<GetArrivalAutocompleteResponse.Root>(res.Result);
                    List<GetArrivalAutocompleteResponse.Item> list = model2.body.items.ToList();
                    List<GetArrivalAutocompleteResponse.Item> list2 = list.Where(item => item.country.id.Equals("TR") && item.hotel != null && (item.city.name.Equals(city) || item.city.name.Equals(enCity))).ToList();
                    cityId = list2[0].city.id.ToString();
                    return cityId;

                }
                return null;
            }
        }
    }
}
