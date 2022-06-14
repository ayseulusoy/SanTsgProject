//HomeController ın view ı olan index sayfasında girilen şehir ismine göre database de bulunan şehir isimlerini arar ve bir sonuç döndürür.
//View ında ise o şehir ismini string olarak alır ve bizi  ListHotelController a yönlendirir.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanTsgProject.Data;
using System.Linq;

namespace SanTsgProject.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly CityDbContext _cityDbContext;

        public SearchController(CityDbContext cityDbContext, ILogger<SearchController> logger)
        {
            _cityDbContext = cityDbContext;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index(string? id)
        {
            var query =from s in _cityDbContext.Cities select s;    
            if (!string.IsNullOrEmpty(id))
            {
                query = _cityDbContext.Cities.Where(p => p.Name.StartsWith(id));

            }
            return View(query.ToList());
        }
    }
}
