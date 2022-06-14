//AccountController kullanıcının email adresi ve şifresiyle sisteme giriş yapamsını sağlar.
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanTsgProject.Data.Interfaces;
using SanTsgProject.Domain.Users;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SanTsgProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _userRepository;
        public AccountController(ILogger<AccountController> logger, IUserRepository userRepository)
        {

            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            _logger.LogInformation("Log message in the 'GET'Login() method");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            _logger.LogInformation("Log message in the 'POST'Login() method");
            var userInDb = _userRepository.Find(x => x.Email == user.Email && x.Password==user.Password);
            //Kullanıcının girdiği mail adresi ve şifresine uygun repository de kullanıcı varsa 
            //Bir talep oluşturulur
            if (userInDb != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var userIdentity=new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal=new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");

            }
            return View();
        }
        
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation("Log message in the LogOut() method");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
