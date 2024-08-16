using FilmCritique.Entities.DbContexts;
using FilmCritique.Entities.Model.Concrete;
using FilmCritique.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmCritique.Models;

namespace FilmCritique.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appuser = _context.AppUsers.SingleOrDefault(c => c.Email == model.Email );
                if (appuser != null)
                {
                    // Oturum açma işlemi
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_context.AppUsers.Any(c => c.Email == model.Email))
                {
                    var appuser = new AppUser
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,                       
                        Email = model.Email,
                      
                    };

                    _context.AppUsers.Add(appuser);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("Email", "This Email is already registered.");
            }

            return View(model);
        }

    }
}
