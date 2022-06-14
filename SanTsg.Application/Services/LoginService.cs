//LoginService apiye login yapmamızı sağlar.
//Bunun için http isteği atmamız gerekir bunu da httplient ile sağlarız. Böylece hep bir istek gönderip hemde bir yanıt alırız.
//İstek göndermek için bir requets modeline ihtiyacımız var bu model içinderki verilerle apiye erişim sağlayablir ve tokenımızı alırız.
using Newtonsoft.Json;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SanTsgProject.Application.Services
{
    public class LoginService : ILoginService
    {
        public async Task<string> GetToken(LoginRequest.Root loginRequest)
        {
            using (var client = new HttpClient())
            {
                string strPayload = JsonConvert.SerializeObject(loginRequest);
                HttpContent httpContent = new StringContent(strPayload, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var Res = await client.PostAsync("http://service.stage.paximum.com/v2/api/authenticationservice/login", httpContent);
                if (Res.IsSuccessStatusCode)
                {
                    var res = Res.Content.ReadAsStringAsync();
                    LoginResponse.Root model = JsonConvert.DeserializeObject<LoginResponse.Root>(res.Result);
                    string token = model.body.token.ToString();
                    return token;
                }
                return null;
            }
        }
    }
}
