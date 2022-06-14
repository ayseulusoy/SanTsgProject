//ListHotelController kullanıcının  seçtiği şehire göre o şehirdeki otelleri listeler. İçerisinde token almak için ILoginService,
//seçile şehrin idsini almak için IGetArrivalAutocompleteServiceve
//otelleri fiyatlarıyla beraber listelemek için IPriceSearchService bulunur.
using Microsoft.AspNetCore.Mvc;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanTsgProject.Web.Controllers
{
    public class ListHotelController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IGetArrivalAutocompleteService _getArrivalAutocompleteService;
        private readonly IPriceSearchService _priceSearchService;
        public ListHotelController(ILoginService loginService, IGetArrivalAutocompleteService getArrivalAutocompleteService, IPriceSearchService priceSearchService)
        {
            _loginService = loginService;
            _getArrivalAutocompleteService = getArrivalAutocompleteService;
            _priceSearchService = priceSearchService;
        }
        public async Task<IActionResult> Index(string? cityName)
        {
            LoginRequest.Root request = new LoginRequest.Root();
            string token = await _loginService.GetToken(request);


            GetArrivalAutocompleteRequest.Root cityIdRequest = new GetArrivalAutocompleteRequest.Root();
            cityIdRequest.Query = cityName;
            string cityId = await _getArrivalAutocompleteService.GetCityId(cityIdRequest, cityName, token);

            List<Hotels> hotels = await _priceSearchService.GetHotels(cityId, token);

            return View(hotels);
        }
    }
}
