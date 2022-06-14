//User Controller bizim kullanıcı işlemlerimizi yapmamızı sağlar.
//Bunları yaparken direkt olarak database kullanılmaz UserRepository aracılığıyla yapılır.
//Database de olan değişiklikleri ise UnitOFWork ile kaydeder.
//Aynı zamanda burada bir mail servisi de kullanılmıştır. Amacı kayıt olan kullanıcıya mail ile bildirim yapmaktır.
//Her operasyon loglanmaktadır.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanTsgProject.Application.Interfaces;
using SanTsgProject.Application.Models;
using SanTsgProject.Data;
using SanTsgProject.Data.Interfaces;
using SanTsgProject.Domain.Users;
using System;
using System.Threading.Tasks;

namespace SanTsgProject.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMailService _mailservice;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(ILogger<UserController> logger, IMailService mailservice, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mailservice = mailservice;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IActionResult Index()
        {
            _logger.LogInformation("Log message in the Index() method");
            var users =_userRepository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Log message in the 'GET'Create() method");
            return View();
        }
        [AllowAnonymous]
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(User user)
        {
            MailRequest mail = new MailRequest()
            {
                Body = "Kaydiniz basariyla alinmistir",
                Subject = "SanTsgProject Kayit Onayi",
                ToEmail = user.Email
            };
            _logger.LogInformation("Log message in the 'POST'Create() method");
            if (ModelState.IsValid)
            {
                user.CreationDate = DateTime.Now.ToShortDateString();
                _userRepository.Add(user);
                _unitOfWork.Complete();
                await _mailservice.SendEmailAsync(mail);
                _logger.LogInformation("Log message in mail send of the 'POST'Create() method");
                return RedirectToAction("Login","Account");
            }
            return View(user);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            _logger.LogInformation("Log message in the 'GET'Edit() method");
            if (id.GetValueOrDefault()==0)
            {
                return NotFound();
            }
            var user = _userRepository.Get(id.Value);
            
            if (user==null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(User user)
        {
            _logger.LogInformation("Log message in the 'POST'Edit() method");
            if (ModelState.IsValid)
            {
                _userRepository.Update(user);
                _unitOfWork.Complete();
                return RedirectToAction("Index","User");
            }
            return View(user);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            _logger.LogInformation("Log message in the Delete() method");
            var user = _userRepository.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            _logger.LogInformation("Log message in the DeletePost() method");
            var user = _userRepository.Get(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Remove(user);
            _unitOfWork.Complete();
            return RedirectToAction("Index","User");
        }

    }
}
