//ShowDetailsController ListHotelController ile listelenen otlellerin otel id sini alarak detaylarını görüntülememiz sağlar.
//Burada token için yeniden istek gönderilir ve token alınır. 
//Alınan token ve hotel id ile çağırılan IGetProductInfoService otelin detaylarını görüntülememizi sağlar.
using Microsoft.AspNetCore.Mvc;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System.Threading.Tasks;

namespace SanTsgProject.Web.Controllers
{
    public class ShowDetailsController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IGetProductInfoService _getProductInfoService;
        public ShowDetailsController(ILoginService loginService, IGetProductInfoService productInfoService)
        {
            _loginService = loginService;
            _getProductInfoService = productInfoService;
        }
        public async Task<IActionResult> Index(string? id)
        {
            LoginRequest.Root request = new LoginRequest.Root();
            string token = await _loginService.GetToken(request);

            GetProductInfoResponse.Hotel hotel =await _getProductInfoService.GetDetails(id, token);

            return View(hotel);
        }
    }
}
